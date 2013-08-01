﻿(function ($) {

    $.fn.jppquad = function (options) {

        var self = this;

        // Create settings object
        var settings = $.extend({
            // Defaults
            createPoints: 0,
            learnPoints: 0,
            explorePoints: 0,
            socializePoints: 0,
            maxThreshold: 100,
            thresholds: {
                0: 4,
                1: 25,
                2: 50
            }
        }, options);

        var findPercent = function( threshold ) {
            return (threshold / settings.maxThreshold).toFixed(2)*100;
        }
        var styleThreshold = function (threshold) {
            var styling = 'width: ' + findPercent(threshold) + '%; '
                        + 'height: ' + findPercent(threshold) + '%; '
                        + 'top: ' + findPercent(settings.maxThreshold/2 - threshold/2) + '%; '
                        + 'left: ' + findPercent(settings.maxThreshold/2 - threshold/2) + '%;';

            return styling;
        }
        var updateQuads = function () {
            self.find('.createQuad').css({ width: findPercent(settings.createPoints / 2) + '%', height: findPercent(settings.createPoints / 2) + '%' });
            self.find('.learnQuad').css({ width: findPercent(settings.learnPoints / 2) + '%', height: findPercent(settings.learnPoints / 2) + '%' });
            self.find('.exploreQuad').css({ width: findPercent(settings.explorePoints / 2) + '%', height: findPercent(settings.explorePoints / 2) + '%' });
            self.find('.socialQuad').css({ width: findPercent(settings.socializePoints / 2) + '%', height: findPercent(settings.socializePoints / 2) + '%' });
        }

        // Ensure proper styling
        this.addClass('jppquad');

        // Build HTML
        // This prevents the view from collapsing upon itself
        this.append('<div class="heightFill"></div>');

        // Draw Background
        this.append('<div class="quadBackground"></div>');


        this.append(
            '<div class="circleContainer">' +
                '<div class="createQuad transition" style="width: 0; height: 0"></div>' +
                '<div class="learnQuad transition"></div>' +
                '<div class="exploreQuad transition"></div>' +
                '<div class="socialQuad transition"></div>' +
            '</div>');
        // Create Quads

        // Draw Thresholds
        // Loop through settings.thresholds
        $.each(settings.thresholds, function (index, value) {
            self.find('.circleContainer').append('<div class="threshold" style="' + styleThreshold( value ) + '"><p>' + value + '</p></div>');
        });
        self.find('.circleContainer').append('<div class="threshold max" style="' + styleThreshold( settings.maxThreshold ) + '"><p>' + settings.maxThreshold + '</p></div>');

        // Draw Counts
        this.append('<div class="createLabel"><p>' + settings.createPoints + '</p></div>');
        this.append('<div class="learnLabel"><p>' + settings.learnPoints + '</p></div>');
        this.append('<div class="exploreLabel"><p>' + settings.explorePoints + '</p></div>');
        this.append('<div class="socialLabel"><p>' + settings.socializePoints + '</p></div>');



        // Enter quad points/animate
        window.setTimeout(updateQuads, 400);


        return this;

    };

}(jQuery));