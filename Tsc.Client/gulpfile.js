/// <binding BeforeBuild='build:scripts' Clean='clean:libs' ProjectOpened='autobuild:scripts' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var mergeStream = require('merge-stream');
var rimraf = require('rimraf');
var ts = require('gulp-typescript');
var sourcemaps = require('gulp-sourcemaps');
var watch = require('gulp-watch');
var tscConfig = require('./tsconfig.json');

var paths = {
    npm: './node_modules/',
    scripts: './app/**/*.ts',
    lib: './lib',
    fonts: './lib/fonts',
    css: './lib/css',
    appdev: './app'
};

var dependencies = [
    paths.npm + 'bootstrap/dist/js/bootstrap*.js',
    paths.npm + 'jquery/dist/jquery*.js'
];

var styleSheets = [
    paths.npm + 'bootstrap/dist/css/bootstrap.css'
];

var fonts = [
    paths.npm + 'bootstrap/dist/fonts/*.*'
];

gulp.task('copy:libs', function () {
    var libStream = gulp.src(dependencies).pipe(gulp.dest(paths.lib));
    var stylesStream = gulp.src(styleSheets).pipe(gulp.dest(paths.css));
    var fontsStream = gulp.src(fonts).pipe(gulp.dest(paths.fonts));
    return mergeStream(libStream, stylesStream, fontsStream);
});

gulp.task('clean:libs', function (callback) {
    rimraf(paths.lib, callback);
});

var tsProject = ts.createProject('tsconfig.json');

gulp.task('build:scripts', function () {
    return tsProject.src(paths.scripts)
                    .pipe(sourcemaps.init())
                    .pipe(ts(tsProject))
                    .pipe(sourcemaps.write('.'))
                    .pipe(gulp.dest(paths.appdev));
});

gulp.task('autobuild:scripts', function () {
    return gulp.watch(paths.scripts, ['build:scripts']);
});
