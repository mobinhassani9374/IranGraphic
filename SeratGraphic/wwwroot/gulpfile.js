var gulp = require('gulp');
var concat = require('gulp-concat');

gulp.task('pack-js', function () {
    return gulp.src(['./files/js/jquery.js',
        './files/js/jquery-migrate.min.js',
        './files/js/jquery.themepunch.tools.min.js',
        './files/js/jquery.themepunch.revolution.min.js',
        './files/js/jquery.blockUI.min.js',
        './files/js/scripts.js',
        './files/js/bootstrap.min.js',
        './files/js/owl.carousel.js',
        './files/js/chosen.jquery.min.js',
        './files/js/chosen.proto.min.js',
        './files/js/jquery.fancybox.pack.js',
        './files/js/jquery.fancybox-buttons.js',
        './files/js/jquery.fancybox-media.js',
        './files/js/jquery.fancybox-thumbs.js',
        './files/js/superfish.min.js',
        './files/js/modernizr.custom.min.js',
        './files/js/jquery.shuffle.min.js',
        './files/js/jquery.mousewheel.min.js',
        './files/js/jquery.countdown.min.js',
        './files/js/waypoints.min.js',
        './files/js/jquery.counterup.min.js',
        './files/js/variables.js',
        './files/js/theme-origine.js',
        './files/js/script.js'
    ])
        .pipe(concat('bundle.js'))
        .pipe(gulp.dest('build/js'));
});

gulp.task('pack-css', function () {
    return gulp.src('./files/css/*.css')
        .pipe(concat('stylesheet.css'))
        .pipe(gulp.dest('build/css'));
});

gulp.task('default', gulp.series('pack-js', 'pack-css'));
