		"use strict";
		var origine_brandnumber = 6,
			origine_brandscrollnumber = 1,
			origine_brandpause = 3000,
			origine_brandanimate = 2000;
		var origine_brandscroll = false;
							origine_brandscroll = true;
					var origine_categoriesnumber = 6,
			origine_categoriesscrollnumber = 2,
			origine_categoriespause = 3000,
			origine_categoriesanimate = 700;
		var origine_categoriesscroll = 'false';
					var origine_blogpause = 3000,
			origine_bloganimate = 700;
		var origine_blogscroll = false;
					var origine_testipause = 3000,
			origine_testianimate = 2000;
		var origine_testiscroll = false;
							origine_testiscroll = false;
					var origine_catenumber = 6,
			origine_catescrollnumber = 2,
			origine_catepause = 3000,
			origine_cateanimate = 700;
		var origine_catescroll = false;
					var origine_menu_number = 9;
		var origine_sticky_header = false;
							origine_sticky_header = true;
					jQuery(document).ready(function(){
			jQuery(".ws").on('focus', function(){
				if(jQuery(this).val()==""){
					jQuery(this).val("");
				}
			});
			jQuery(".ws").on('focusout', function(){
				if(jQuery(this).val()==""){
					jQuery(this).val("");
				}
			});
			jQuery(".wsearchsubmit").on('click', function(){
				if(jQuery("#ws").val()=="" || jQuery("#ws").val()==""){
					jQuery("#ws").focus();
					return false;
				}
			});
			jQuery(".search_input").on('focus', function(){
				if(jQuery(this).val()==""){
					jQuery(this).val("");
				}
			});
			jQuery(".search_input").on('focusout', function(){
				if(jQuery(this).val()==""){
					jQuery(this).val("");
				}
			});
			jQuery(".blogsearchsubmit").on('click', function(){
				if(jQuery("#search_input").val()=="" || jQuery("#search_input").val()==""){
					jQuery("#search_input").focus();
					return false;
				}
			});
		});
		