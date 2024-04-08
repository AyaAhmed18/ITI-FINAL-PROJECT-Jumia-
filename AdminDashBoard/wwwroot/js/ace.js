(function($) {
  'use strict';
  var editor = ace.edit("aceExample");
  editor.Theme("ace/theme/chaos");
  editor.getSession().Mode("ace/mode/javascript");
  document.getElementById('aceExample').style.fontSize = '1rem';
})(jQuery);