(function($) {
  'use strict';
  $.fn.easyNotify = function(options) {

    var tings = $.extend({
      title: "Notification",
      options: {
        body: "",
        icon: "",
        lang: 'pt-BR',
        onClose: "",
        onClick: "",
        onError: ""
      }
    }, options);

    this.init = function() {
      var notify = this;
      if (!("Notification" in window)) {
        alert("This browser does not support desktop notification");
      } else if (Notification.permission === "granted") {

        var notification = new Notification(tings.title, tings.options);

        notification.onclose = function() {
          if (typeof tings.options.onClose === 'function') {
            tings.options.onClose();
          }
        };

        notification.onclick = function() {
          if (typeof tings.options.onClick === 'function') {
            tings.options.onClick();
          }
        };

        notification.onerror = function() {
          if (typeof tings.options.onError === 'function') {
            tings.options.onError();
          }
        };

      } else if (Notification.permission !== 'denied') {
        Notification.requestPermission(function(permission) {
          if (permission === "granted") {
            notify.init();
          }

        });
      }

    };

    this.init();
    return this;
  };


  //Initialise notification
  var myFunction = function() {
    alert('Click function');
  };
  var myImg = "https://unsplash.it/600/600?image=777";

  $("form").submit(function(event) {
    event.preventDefault();

    var options = {
      title: $("#title").val(),
      options: {
        body: $("#message").val(),
        icon: myImg,
        lang: 'en-US',
        onClick: myFunction
      }
    };
    console.log(options);
    $("#easyNotify").easyNotify(options);
  });
}(jQuery));