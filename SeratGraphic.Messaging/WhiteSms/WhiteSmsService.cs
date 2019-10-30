using Microsoft.Extensions.Options;
using SeratGraphic.Messaging.WhiteSms.Models;
using SeratGraphic.Messaging.WhiteSms.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeratGraphic.Messaging.WhiteSms
{
    public class WhiteSmsService
    {
        private readonly Functions _functions;
        public readonly RequestProvider _requestProvider;
        private readonly Key _key;
        public WhiteSmsService(Functions functions,
            RequestProvider requestProvider,
            IOptions<Key> options)
        {
            _functions = functions;
            _requestProvider = requestProvider;
            _key = options.Value;
        }
        //public async Task<IResponse> SendAsync(List<string> phoneNumbers, string message)
        //{
        //    var token = await _functions.GetToken(_key);

        //    if (token.IsSuccessful)
        //    {
        //        foreach (var mobile in phoneNumbers)
        //        {
        //            var searchResult = await _functions.SearchContacts(new SearchContactModel
        //            {
        //                Mobile = mobile
        //            }, token.TokenKey);

        //            if (searchResult.Contacts != null && searchResult.Contacts.Count == 0)
        //            {
        //                var addContactResult = await _functions.AddContact(new AddContactModel
        //                {
        //                    GroupId = 28450,
        //                    ContactsDetails = new List<ContactsDetailModel>()
        //                      {
        //                           new ContactsDetailModel
        //                           {
        //                                Mobile=mobile,
        //                                 FirstName=mobile,
        //                                  LastName=mobile,
        //                                   Prefix="آقا"
        //                           }
        //                      }
        //                }, token.TokenKey);
        //            }
        //        }

        //        var sendResult = await _functions.SendByMobileNumbers(new SendByMobileNumberModel
        //        {
        //            Message = message,
        //            MobileNumbers = phoneNumbers,
        //            CanContinueInCaseOfError = false
        //        }, token.TokenKey);
        //        return sendResult;
        //    }
        //    return new Response()
        //    {
        //        IsSuccessful = false,
        //        ErrorCode = -1,
        //        Message = $"خطا در دریافت توکن {token.Message}"
        //    };
        //}

        public async Task<IResponse> SendAsync(string message,params string[] phoneNumbers )
        {
            var token = await _functions.GetToken(_key);

            if (token.IsSuccessful)
            {
                foreach (var mobile in phoneNumbers)
                {
                    var searchResult = await _functions.SearchContacts(new SearchContactModel
                    {
                        Mobile = mobile
                    }, token.TokenKey);

                    if (searchResult.Contacts != null && searchResult.Contacts.Count == 0)
                    {
                        var addContactResult = await _functions.AddContact(new AddContactModel
                        {
                            GroupId = 28450,
                            ContactsDetails = new List<ContactsDetailModel>()
                              {
                                   new ContactsDetailModel
                                   {
                                        Mobile=mobile,
                                         FirstName=mobile,
                                          LastName=mobile,
                                           Prefix="آقا"
                                   }
                              }
                        }, token.TokenKey);
                    }
                }

                var sendResult = await _functions.SendByMobileNumbers(new SendByMobileNumberModel
                {
                    Message = message,
                    MobileNumbers = phoneNumbers.ToList(),
                    CanContinueInCaseOfError = false
                }, token.TokenKey);
                return sendResult;
            }
            return new Response()
            {
                IsSuccessful = false,
                ErrorCode = -1,
                Message = $"خطا در دریافت توکن {token.Message}"
            };
        }
    }
}
