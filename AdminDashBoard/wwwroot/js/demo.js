(function($) {
  'use strict';
  $(function() {
    $("#features-link").on("click", function() {
        $('html, body').animate({
            scrollTop: $("#features").off().top
        }, 1000);
    });
  });
})(jQuery);