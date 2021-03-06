﻿/*
 * JPP Timeline - Jquery plugin
 * Handles the creation and update of a Timeline Feed
 * Just Press Play (http://play.rit.edu/)
 * 2013
 * Probably on a MIT license
 */

// TODO: Minifiy!

(function ($) {

    $.fn.jpptimeline = function (options) {

        // Selectors
        var self = $(this);
        var $header;
        var $bottom;
        var $spinner;
        var $feed;
        var $loggedUserInfo;


        var currentUserID = null;
        var selectorClass = '.playerSelector';
        var timelineClass = 'timelineFeed';


        var earningCount = 0;                           // The current number of earnings displayed
        var ajaxData = null;
        var scrollBuffer = 200;

        // Create settings object
        var settings = $.extend({
            userID: null,                   // userID is the logged-in user visiting this page
            achievementID: null,
            questID: null,
            baseURL: null,
            startIndex: 0,
            loadInterval: 5,
            scrollBuffer: 200,
            dynamicUser: false
        }, options);

        scrollBuffer = settings.scrollBuffer;



        /* FUNCTIONS */

        // Sets timeline variables back to initial state and clears containing div
        var reset = function () {
            currentUserID = null;
            earningCount = 0;
            ajaxData = null;

            clear();
        };

        // Clears containing div
        var clear = function () {
            $feed.html('');
            $header.html('');
        }

        // Determines if a value n is a number
        // @param n Unitless value to check
        // @return boolean true/false
        // From: http://stackoverflow.com/questions/18082/validate-numbers-in-javascript-isnumeric/1830844#1830844
        var isNumber = function (n) {
            return !isNaN(parseFloat(n)) && isFinite(n);
        }

        // Reloads timeline for a new userID and other setting variables
        // @param userID Number id for a user
        $.fn.jpptimeline.loadUser = function (userID) {

            console.log('loadUser called');

            // Don't load if we are already displaying the user's data
            if (userID !== currentUserID) {

                reset();

                $spinner.show();

                currentUserID = userID;

                $.fn.jpptimeline.load();
            }
        };
        

        // Loads earnings based upon settings object and currentUserID, appends to parent div
        $.fn.jpptimeline.load = function () {

            //console.log('load Called');

            // Load where we left off
            settings.startIndex = earningCount;

            // Make sure the user id is a number
            if (!isNumber(currentUserID)) {
                currentUserID = null;
            }


            // Determine if the submission form should be shown
            // TODO: Check if it is a submission type
            //          - Only show on currently logged user if they have not recieved the achievement
            if (currentUserID == null || currentUserID == settings.userID && !$loggedUserInfo.hasClass('earned')) {
                $loggedUserInfo.show();
            }
            else {
                $loggedUserInfo.hide();
            }

            // Only show the loading spinner if there is probably more stuff to load
            if (earningCount % settings.loadInterval == 0)
                $spinner.show();

            $.ajax({
                url: buildAjaxURL(),
                dataType: "json",
                type: "POST",
                success: function (data) {

                    ajaxData = data;


                    // Append headings
                    if (earningCount == 0)
                    {
                        // Dynamic feed and user is null
                        if (settings.dynamicUser && currentUserID == null) {
                            $header.append('<h3>PUBLIC FEED</h3>');
                        }
                        // Dynamic feed and user is not null
                        else if (settings.dynamicUser && currentUserID != null) {

                            if (data.Earnings.length > 0)
                            {
                                $header.append('<h3>' + (data.Earnings[0].PlayerID == settings.userID ? 'YOUR' : data.Earnings[0].DisplayName + '\'s') + ' EARNINGS</h3>');
                            }
                                
                            else if (currentUserID != settings.userID )
                                $header.append('<h3>Somebody\'s FEED</h3>');
                        }
                    }
                    
                       

                    buildEarnings();

                    // Hide loading div after earnings have been appended
                    $spinner.hide();

                    // Only set the hash if needed
                    if (settings.dynamicUser && currentUserID != null) location.hash = currentUserID;

                    // Clear loaded data
                    delete ajaxData;

                },
                error: function (e) {

                    $spinner.hide();

                    // Error message
                    $feed.append('<div style="text-align: center; margin-bottom: 0.9375em;">' +
                                    '<span class="radius alert label expand">Whoops! Something went wrong! As part of our apology try clicking <a href="#" onclick="cornify_add();return false;">this link</a><script type="text/javascript" src="http://www.cornify.com/js/cornify.js"></script>.</span>' +
                                '</div>');

                    return false;
                }
            });

            // Don't place anything here; ajax calls are async and we don't know when they finish
        };



        /*#region Build Functions*/

        // Builds the URL to be queried by Ajax
        // @return: URL String
        var buildAjaxURL = function() {
            var query = settings.baseURL + '/JSON/Earnings?';

            if(settings.questID != null)
                query += 'questID=' + settings.questID + '&';

            if(settings.achievementID != null)
                query += 'achievementID=' + settings.achievementID + '&';


            if (currentUserID != null)
            {
                query += 'id=' + currentUserID + '&';
            }
            else if (!settings.dynamicUser && settings.userID != null)
            {
                query += 'id=' + settings.userID + '&';
            }
            

            if(settings.startIndex != null && settings.startIndex >= 0)
                query += 'start=' + settings.startIndex + '&';

            if(settings.loadInterval != null && settings.loadInterval >= 0)
                query += 'count=' + settings.loadInterval + '&';

            return query;
        };

        
        // Builds every possible earning for loadInterval or count, whichever has a lower value
        var buildEarnings = function() {

            var earnings = ajaxData.Earnings;

            // For the number of earnings to build
            for ( var i = 0; i < ( settings.loadInterval < earnings.length ? settings.loadInterval : earnings.length); i++ ) {

                // Check for a change in user
                /*
                if (settings.dynamicUser && settings.userID != earnings[i].playerID)
                {
                    $.fn.jpptimeline.loadUser(settings.userID);
                    return;
                }
                */

                // Build each
                $feed.append(buildEarning(earnings[i]));

                ++earningCount;

            }
            if (earnings.length > 0) {
                //$(self).append('<div class="timelinePost"><p>This user hasn\'t earned this item yet<p></div>');
                $(window).bind('scroll', bindScroll);
            }
            if (earnings.length == 0 && earningCount <= 0 && currentUserID != settings.userID) {
                $feed.append('<div class="timelinePost"><div class="postBody"><p style="margin: 0;">This user hasn\'t earned this item yet</p></div></div>');
                ++earningCount;
            }
        };


        // Builds a single earning
        // @param: Specific earning data object
        // @return: jQuery element
        var buildEarning = function (earningData) {

            // Create post container
            $earningDiv = $(document.createElement('div')).addClass('timelinePost');

            // Insert Photo
            if (earningData.EarningIsAchievement &&  ( earningData.StoryPhoto != null && earningData.StoryPhoto != "") ) {
                $earningDiv.append('<div class="postPhoto"><img src="' + settings.baseURL + earningData.StoryPhoto.replace(/\\/g, "/").substr(1) + '" align="middle" /></div>');
            }

            // Insert Link


            // Insert post info
            //console.log('Build post info');
            $earningDiv.append(buildPostInfo(earningData));

            // Insert post body
            //console.log('Build post body');
            //if (earningData.EarningIsAchievement)
                $earningDiv.append(buildPostBody(earningData));


            return $earningDiv;

        }


        // Builds earning information header
        // @param: Specific earning data object
        // @return: jQuery element
        var buildPostInfo = function (data) {
            $postInfoDiv = $(document.createElement('div')).addClass('postInfo');

            var imageSrc = '';

            // TODO: Replace with updated image verification

            if (data.PlayerImage == null) {
                imageSrc = '/Content/Images/Jpp/defaultProfileAvatar.png';
            }
            else {
                imageSrc = data.PlayerImage.substr(1);
                imageSrc = imageSrc.replace(/\.([^.]+)$/, '_s.$1');
            }

            $postInfoDiv.append(    '<div class="userPhoto">' +
                                        '<a href="' + settings.baseURL + '/Players/' + data.PlayerID + '">' +
                                            '<img src="' + settings.baseURL + getImageURL(data.PlayerImage, 's') + '" />' +
                                        '</a>' +
                                    '</div>');


            $postInfoDiv.append('<h1>' +
                                    '<a href="' + settings.baseURL + '/Players/' + data.PlayerID + '">' +
                                        data.DisplayName +
                                    '</a>' +
                                '</h1>');
            $postInfoDiv.append('<h2>' +
                                    'Earned <a href="' + settings.baseURL + '/Achievements/' + data.TemplateID + '#' + data.PlayerID + '">' + data.Title + '</a>' +
                                '</h2>');
            var date = new Date(parseInt(data.EarnedDate.substr(6))).toLocaleDateString();
            $postInfoDiv.append('<h3>' +
                                    date +
                                '</h3>');

            return $postInfoDiv;
        };


        // Builds earning body including comments
        // @param: Specific earning data object
        // @return: jQuery element
        var buildPostBody = function (data) {
            $postBodyDiv = $(document.createElement('div')).addClass('postBody');

            
            //if (data.StoryText != null && data.StoryText != "")
            //{
            var story = (data.StoryText == null) ? data.DisplayName + ' hasen\'t added a story yet.' : data.StoryText;
            $postBodyDiv.append('<p>' + story + '</p>');
            //}

            //$postBodyDiv.append('<hr />');


            // TODO: Privacy and logged user commenting
            // Only add comments if they are not disabled
            if(!data.CommentsDisabled) $postBodyDiv.append(buildCommentsContainer(data));


            return $postBodyDiv;
        }



        // Creates the container that holds comments on a specific earning
        // @param: Earning data object
        // @return: jQuery element
        var buildCommentsContainer = function (data) {

            $commentsContainer = $(document.createElement('div')).addClass('commentsContainer');
            $commentsContainer.attr('data-earningID', data.EarningID);

            //$commentsContainer.append('<h5>' + data.Comments.length + ' Comments</h5>');
            $commentsContainer.append('<h5>Comments</h5>');


            // TODO: Load comments on scroll/click on "see more"
            // Display every comment
            for (var i = 0; i < data.Comments.length; i++)
            {
                
                $commentsContainer.append(buildComment(data.Comments[i]));
                
                // TODO: Add options on comment hover if user has permissions
            }



            // TODO: Add comment textbox if user has permission to comment
            var $commentSubmit = $(document.createElement('form')).addClass('writeComment');
            $commentSubmit.attr('action', '/Comments/Add').attr('method', 'post');
            
            // Write comment box
            $commentSubmit.append(  '<input name="earningID" type="hidden" value="' + data.EarningID + '" />' +
                                    '<input name="earningIsAchievement" type="hidden" value="' + data.EarningIsAchievement + '" />' +
                                    /*
                                    '<div class="userPhoto hide-for-small">' +
                                        '<img src="' + settings.baseURL + '/Content/Images/Jpp/defaultProfileAvatar.png" />' +
                                    '</div>' +
                                    */
                                    '<div class="commentInput">' +
                                        '<input name="text" type="text" placeholder="Write a comment..." />' +
                                    '</div>');

            // Submit the comment with ajax
            $commentSubmit.keypress(function (e) {

                console.log('keypress');

                if (e.keyCode == 13 && !e.shiftKey) {

                    var form = $(this);

                    // Get comment string
                    var commentString = form[0].text.value;

                    $.ajax({
                        type: form.attr('method'),
                        url: form.attr('action'),
                        data: form.serialize(),
                        success: function (newData) {
                            // Clear form input
                            form[0].text.value = '';
                            // Append new comment before this form
                            form.before(buildComment(newData));
                        }
                    });

                    e.preventDefault();
                }

            });

            $commentsContainer.append($commentSubmit);

            return $commentsContainer;
        }


        // Generates DOM elements for a single comment
        // Binds click events to moderation options
        // @param: data - JSON formatted comment data
        // @return: jQuery element
        var buildComment = function (data) {

            var $comment = $(document.createElement('div')).addClass('comment').attr('data-commentID', data.ID);
            if (data.Deleted) $comment.addClass('deleted');
            $comment.append('<div class="image">' +
                                '<a href="#"><img src="' + settings.baseURL + getImageURL(data.PlayerImage, 's') + '" /></a>' +
                            '</div>' +

                            '<div class="body">' +
                                '<p class="name">' +
                                    '<a href="#">' + data.DisplayName + '</a>' +
                                '</p>' +
                                '<div class="text">' +
                                    '<p>' + data.Text + '</p>' +
                                '</div>' +
                                '<div class="edit"></div>' +
                            '</div>');

            var $textDiv = $comment.children('.body').children('.text');
            var $editDiv = $comment.children('.body').children('.edit');


            // If the comment hasn't been deleted we should see options
            if (!data.Deleted)
            {
                var $options = $(document.createElement('div')).addClass('options');

                var $edit = $(document.createElement('a'));

                $edit.click(function (e) {
                    e.preventDefault();
                    $options.hide();

                    // Generate edit form
                    var $editForm = $(document.createElement('form')).attr('action', '/Comments/Edit').attr('method', 'post');
                    $editForm.append('<input name="commentID" type="hidden" value="' + data.ID + '" />' +
                                    '<textarea name="text">' + $textDiv.text() + '</textarea>' +
                                    '<input name="submit" type="submit" value="Save" />');
                    // Bind edit submission
                    $editForm.submit(function (e) {

                        var form = $(this);

                        $.ajax({
                            type: form.attr('method'),
                            url: form.attr('action'),
                            data: form.serialize(),
                            success: function (data) {
                                
                                $textDiv.html('<p>' + data.CommentText + '</p>');
                                $editDiv.html('');
                                $options.show();
                                //$comment.remove();
                                console.log('Comment Edited');
                            }
                        });

                        e.preventDefault();

                    });

                    $textDiv.html('');
                    $editDiv.append($editForm);

                }); // End $edit click

                // Generate delete form
                var $delete = $(document.createElement('a'));

                $delete.click(function (e) {
                    e.preventDefault();

                    var $deleteForm = $(document.createElement('form')).attr('action', '/Comments/Delete').attr('method', 'post');
                    $deleteForm.append('<input name="commentID" type="hidden" value="' + data.ID + '" />');

                    $.ajax({
                        type: $deleteForm.attr('method'),
                        url: $deleteForm.attr('action'),
                        data: $deleteForm.serialize(),
                        success: function (data) {
                            $comment.addClass('deleted');
                            $options.remove();
                            console.log('Comment Deleted');
                        }
                    });

                });

                $edit.addClass('jppico-file');
                $delete.addClass('jppico-close');

                // Append options
                $options.append($edit);
                $options.append($delete);
                $comment.append($options);
            }
            


            return $comment;

        };

        /*#endregion*/

        
        
        // TODO: Event listeners for comment button clicks



        // Generates a valid image path for a supplied src
        // @param imgSrc Base url image can be found at
        // @param size Optional desired size (s, m)
        // @return Valid url for the given src and size
        var getImageURL = function (imgSrc, size)
        {
            var imageSrc = '';

            if (imgSrc == null) {
                imageSrc = '/Content/Images/Jpp/defaultProfileAvatar.png';
            }
            else {
                imageSrc = imgSrc.replace(/\\/g, "/").substr(1);
                if( size != null) imageSrc.replace(/\.([^.]+)$/, '_' + size + '.$1');
            }

            return imageSrc;
        }

        // Callback for scroll event to handle additional loading
        var bindScroll = function () {
            // Check for a scroll to the bottom of the timelineFeed - scrollBuffer
            if ($(window).scrollTop() + $(window).height() >= self[0].scrollHeight + self.offset().top - scrollBuffer) {
                $(window).unbind('scroll');
                $.fn.jpptimeline.load();
            }
        };



        // INIT
        var init = function () {

            if (settings.userID < 0)
                settings.userID = null;

            // Look for a hashed id on load
            if (settings.dynamicUser && location.hash.length > 1) {
                console.log('init hash: ' + location.hash.substr(1));
                currentUserID = location.hash.substr(1);
            }

            // Add the heading
            if (settings.dynamicUser)
            {
                var $tempHeader = $(document.createElement('div')).addClass('timelineHeading');
                self.append($tempHeader);
                $header = self.children('.timelineHeading');
            }

            // Create feed container
            var $tempFeed = $(document.createElement('div')).addClass('timelineFeed');
            self.append($tempFeed);
            $feed = self.children('.timelineFeed');

            // Add bottom with spinner
            var $bot = $(document.createElement('div')).addClass('bottom');
            $bot.append('<div class="spinner"></div>');
            $bot.append('<div class="endOfFeed"><h6>End of Feed</h6><div>');
            self.append($bot);
            $bottom = self.children('.bottom');
            $spinner = $bottom.children('.spinner');

            // Store the loggedUserInfo element
            $loggedUserInfo = $('#loggedUserInfo');

            console.log('Init userID: ' + settings.userID);

            // Initial load
            $.fn.jpptimeline.load();
        }

        init();
        

        //window.onhashchange = $.fn.jpptimeline.loadUser(location.hash.substring(1));




        $(window).on('hashchange', function () {

            var hash = location.hash.substring(1);

            if (settings.dynamicUser && currentUserID != hash) {
                console.log('hash change: ' + hash);
                $.fn.jpptimeline.loadUser(hash);
            }

        });


        $spinner.ajaxStart(function () {
            $spinner.show();
        });

        $(selectorClass).click(function () {
            if ($.active <= 0) {
                console.log('player selector click: ' + $(this).attr('data-userID'));
                $.fn.jpptimeline.loadUser($(this).attr('data-userID'));
            }
            else
                console.log('Active ajax request: Could not process request');
        });


        return this;

    };

}(jQuery));
