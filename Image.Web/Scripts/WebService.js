function loadjscssfile(filename, filetype) {
    if (filetype == "js") { //if filename is a external JavaScript file
        var fileref = document.createElement("script");
        fileref.setAttribute("type", "text/javascript");
        fileref.setAttribute("src", filename);
    }
    else if (filetype == "css") { //if filename is an external CSS file
        var fileref = document.createElement("link");
        fileref.setAttribute("rel", "stylesheet");
        fileref.setAttribute("type", "text/css");
        fileref.setAttribute("href", filename);
    }
    if (typeof fileref != "undefined")
        document.getElementsByTagName("head")[0].appendChild(fileref);
} 

function toStringFA(o)
{
    if(o.toString() == '[object Window]') return;
    Str = '{';
    for (var key in o) 
    {
      if(!o.hasOwnProperty || o.hasOwnProperty(key)) 
      { 
        if(o[key] != null )
        if(typeof(o[key]) == 'object')
            Str += "'"+key+ "':"+toStringFA(o[key])+",";
        else
        {
            temp = "'";
            if (typeof (o[key]) == 'boolean' || typeof (o[key]) == 'number') temp = "";
            else if (o[key] != null && typeof (o[key]) == 'string') { o[key] = o[key].replace(/'/gi, "\\'"); }
            Str += "'"+key +"':"+temp+o[key]+temp+",";
        }
      }
    }
    if(Str.length > 1) Str = Str.substring(0,Str.length-1) ;
    return Str+="}";
}
var cache = [];
function CheckCache(url, datao) { if (typeof (datao) == 'object') { datao = toStringFA(datao); } return cache[datao]; }

function GetActionHtml(url, datao, onsuccess, lid, usecache, LoadingUrl) {
    GoToServerBase(false, 'POST', url, datao, onsuccess, lid, usecache, LoadingUrl, true);
}
function GoToServer(url, datao, onsuccess, lid, usecache, LoadingUrl) {
    GoToServerBase(false, 'POST', url, datao, onsuccess, lid, usecache, LoadingUrl, false);
}
function CrossGet(url, datao, onsuccess, lid, usecache, LoadingUrl) {
    GoToServerBase(true, 'GET', url, datao, onsuccess, lid, usecache, LoadingUrl);
}
function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}
function GoToServerBase(IsCross,type,url, datao, onsuccess, lid, usecache, LoadingUrl,html,ShowMessage) {
    if (usecache == null) usecache = false;
    if (ShowMessage == null) ShowMessage = true;
    
    if (LoadingUrl == null) LoadingUrl = 'http://Img.Tebyan.net/MainParts/Persian/Thesarus/Tcore/loading.gif';
    if (datao != null && typeof(datao) == 'object') { datao = toStringFA(datao); }
    
    if (typeof (lid) == 'boolean' && lid == true)
    {
        lid = function (a) {
            if (a) {
                $('.LoadingBox').show();
            }
            else $('.LoadingBox').hide();
        }
    }
    if (onsuccess == null)
    {
        onsuccess = function () { };
    }
    if (usecache && cache[url + datao] != null) { return onsuccess(cache[url + datao], 'fromcache'); }
    try
    {
        jQuery.ajax({
            type: type, url: url,
            contentType: (IsCross && type != 'POST' ? null : IsCross && type == 'POST' ? 'application/x-www-form-urlencoded; charset=utf-8' : 'application/json; charset=utf-8'),
            dataType: html ? "html" : "json", data: datao,
            xhrFields: {
                withCredentials: true
            },
            success: function (data, s) {
                if (typeof (data) != 'object') {
                    data = { d: data };
                }
                else {
                    if (data == null) data = {};
                    if (data.d == null) data.d = data;
                }

                if (ShowMessage && data.d.IsMessage)
                {
                    $('body').Notification({ TxtMSG: data.d.Message, TypeMessage: data.d.Type == 1 ? 'ErrorMSG' : 'SuccessMSG', HideTimeOut: 1000 });
                    if (data.d.MessageCode == 7) {
                        document.location.href = "/fa/Common/RequestCaptcha?c=" + data.d.Data.c + "&a=" + data.d.Data.a;
                    }
                    data.d = data.d.Data;
                    cache[url + datao] = data;
                    onsuccess(data, s);
                }
                else if (data.d.ErrorMessage) {
                    alert(data.d.ErrorMessage);
                }
                else {
                    cache[url + datao] = data;
                    onsuccess(data, s);
                }
                if (lid != null) {
                    if (typeof (lid) == 'function') {
                        lid(false);
                    }
                    else {
                        $('.js_LoadingBox').fadeOut();
                        lid.parent().find('.loadingDiv').fadeOut(1, function () { $(this).remove(); $('.parentDiv1389').css({ "position": "static" }); });
                    }
                    // lid.parent().css({ position: 'static' });
                }
                else {
                    $('.js_LoadingBox').fadeOut();
                }
            },
            beforeSend: function (request) {
                request.setRequestHeader('userToken', getParameterByName('userToken'));
                if (lid != null) {
                    if (typeof (lid) == 'function') {
                        lid(true);
                    }
                    else {
                        $('.js_LoadingBox').fadeIn();
                        if (!lid.parent().hasClass('parentDiv1389'))
                            lid.wrap('<div class="parentDiv1389" style="position:relative;"></div>');
                        else $('.parentDiv1389').css({ 'position': 'relative' });
                        parentDiv = lid.parent('.parentDiv1389');

                        parentDiv.append('<div class="loadingDiv"></div>');
                        LoadingDiv = parentDiv.find('.loadingDiv');
                        LoadingDiv.css({ 'position': 'absolute', 'top': '0px', 'left': '0px', 'width': lid.width(), 'height': '100%', 'background': '#EEE url(' + LoadingUrl + ') no-repeat center', 'z-index': '100', 'opacity': '0.5' });
                    }

                }
                else {
                    $('.js_LoadingBox').fadeIn();
                }
            },
            error: function (xmlHttpRequest, status, err) {
                if (lid != null) {
                    if (typeof (lid) == 'function') {
                        lid(false);
                    }
                    else {
                        $('.js_LoadingBox').fadeOut();
                        lid.parent().find('.loadingDiv').fadeOut(1, function () { $(this).remove(); $('.parentDiv1389').css({ 'position': 'static' }) });
                        lid.parent().css({ position: 'static' });
                    }
                }
                else {
                    $('.js_LoadingBox').fadeOut();
                }
            }
        });
    }
    catch(e){}
}
function CheckLogin() {
    if ($('#lbtnLogout').is('a'))
    GoToServer("/WebServices/BaseWebService.asmx/Login", '', function (data) {
        if (data.d == false) {
            window.location.reload();
        }
    }, null, false);
    setTimeout('CheckLogin()', 120000);
}
setTimeout('CheckLogin()', 120000);




var Areas = '';
var AncorArray = new Array();
var AncorAreas = new Array();
function ManageHash(_Areas) {
    Areas = _Areas;
    AncorAreas = Areas.split(',');
    document.location.hash = '#' + AncorArray.length;
}
function UpdateHash() {
    if (document.location.hash == '') {
        document.location.reload();
        return;
    }
    var u = document.location.hash.substring(1);
    if (AncorArray[u] != null) {

        var NewArray = new Array();
        for (var p = 0; p < AncorAreas.length; ++p) {
            NewArray[p] = $(AncorAreas[p]).html();
        }
        AncorArray[u] = NewArray;
    }
}
$(window).bind('hashchange', function () {
    if (document.location.hash == '') {
        document.location.reload();
        return;
    }
    var u = document.location.hash.substring(1);
    if (AncorArray[u] == null) {
        var NewArray = new Array();
        for (var p = 0; p < AncorAreas.length; ++p) {
            NewArray[p] = $(AncorAreas[p]).html();
        }
        AncorArray[u] = NewArray;
    }
    else {
        for (var p = 0; p < AncorArray[u].length; ++p) {
            $(AncorAreas[p]).html(AncorArray[u][p]);
        }
    }
});