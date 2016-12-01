$.widget("ui.Anuncio", {
    // default options
    options: {
        texto: '',
        buttons: {
        
        },
        vposition: 'top',
        hposition: 'right'
    },

    // The constructor
    _create: function () {

    },
    getContent: function () {
        var $this = $(this);

        var content = $('<div class="ad-' + $this.options.vposition + ' ad-' + $this.options.hposition + '">' +
                        
                        '</div>');
        $("body").append();
    },
    _destroy: function () {

    }
});