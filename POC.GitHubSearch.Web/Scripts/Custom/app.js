var app = [];
app.searchUrl = 'https://localhost:44325/api/repo/search/min?';
app.searchResults = [];

var bookmarks = [];
bookmarks.Url = 'https://localhost:44325/api/bookmarks';
bookmarks.attachEventSet = function () {
    $('div.slick-list button.bookmark').click(function () {
        var id = $(this).attr('id');

        $.ajax({
            method: 'POST',
            url: bookmarks.Url + '/' + id,
            data: {},
            beforeSend: function (xhr) {
                
            },
            success: function (data) {
                alert('Repository ' + id + ' bookmarked successfully!');
            },
            error: function (xhr, status, error) {
                console.log('statusText: ' + xhr.statusText);
            }
        });
    });
};
bookmarks.get = function() {
    $.ajax({
        method: 'GET',
        url: bookmarks.Url,
        data: {  },
        beforeSend: function (xhr) {
            
        },
        success: function (data) {
            
        },
        error: function (xhr, status, error) {
            console.log('statusText: ' + xhr.statusText);
        }
    });
}


var search = [];
search.attachEvent = function () {
    $('#btn-search').click(function () {
        $.ajax({
            method: 'GET',
            url: app.searchUrl,
            data: { query: encodeURIComponent($('#query').val()) },
            beforeSend: function (xhr) {
                // EMPTY LAST RESULTS IF ANY BEFORE POPULATING NEW RESULTS
                $('.jumbotron.blue').hide();
                $('.avatarGallery').slick('unslick');
                app.locationsSearchResults.empty();               
            },
            success: function (data) {
                app.searchResults = data;
                search.populateResults(app.searchResults);
                bookmarks.attachEventSet();
                $('.jumbotron.blue').show();
            },
            error: function (xhr, status, error) {
                console.log('statusText: ' + xhr.statusText);
            }
        });
    });
};
//
search.populateResults = function (results) {
    if (results)  {
        //
        results.RepositoryItems.forEach(function (item, index) {
            // GET TEMPLATE CLONE
            var itemTemplate = $($('#repo-item-template')[0].outerHTML).clone();
            // REMOVE ID / CLASS FROM TEMPLATE GENERATION
            itemTemplate.removeAttr('id');
            itemTemplate.removeClass('template');
            // ASSIGN AVATAR 
            var avatar = $(itemTemplate).find("img.avatar");
            $(avatar[0]).attr("src", item.AvatarUrl)[0];
            // ASSIGN REPOSITORY NAME AND ID
            var name = $(itemTemplate).find("span.label");
            $(name[0]).text(item.Name);
            //
            var bookmark = $(itemTemplate).find("button.bookmark");
            $(bookmark[0]).attr('id', item.Id);
            // ATTACH TO DESIGNATED PLACE
            itemTemplate.appendTo(app.locationsSearchResults);
            // REMOVE THE HIDDEN CLASS
            itemTemplate.removeClass('hidden');
        });
        //
        slickSettings.initialize();
        //
        $($("div.jumbotron.blue.hidden")[0]).removeClass('hidden');
    }
};

var slickSettings = [];
slickSettings.slidesToShow = 5;
slickSettings.slidesToScroll = 5;

slickSettings.initialize = function () {
    $('.avatarGallery').not('.slick-initialized').slick({
        accessibility: true,
        adaptiveHeight: false,
        arrows: true,
        cssEase: 'ease',
        dots: true,
        dotsClass: 'slick-dots',
        focusOnSelect: false,
        initialSlide: 0,
        lazyLoad: 'progressive',
        rows: 1,

        infinite: true,
        slidesToShow: slickSettings.slidesToShow,
        slidesToScroll: slickSettings.slidesToScroll
    });
};


$(function () {
    app.locationsSearchResults = $($("div.avatarGallery.locationsSearchResults")[0]);
    search.attachEvent();
    slickSettings.initialize();
});

$(document).on({
    ajaxStart: function () {
        $("body").addClass("loading");
    },
    ajaxStop: function () {
        $("body").removeClass("loading");
    }
});