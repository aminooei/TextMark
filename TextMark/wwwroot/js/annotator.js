/**
 * @author      Deepak K Anand
 * @version     1.0.0 (Beta)
 * @todo        (a) Add Comments
 *              (b) Fix bugs
 *              (c) Git Repo
 *              (d) Performance Testing
 */

(function ($) {

    $.fn.annotator = function (options) {
        var self = this;
        
        this.constants = {
            errorCodes: {

            }
        };

        this.defaults = {
            surroundWith: "annotation",
            minimumCharacters: 1,
            makeTextEditable: true
        };

        if (!window.Popper)
            throw Error("Annotate.js requires Popper");

        if (!options)
            throw Error("Missing configuration options");

        if (!options.popoverContents)
            throw Error("Missing option: popoverContents");

        if (jQuery(options.popoverContents).length === 0)
            throw Error(`Missing popoverContents element on DOM: ${options.popoverContents}`);

        var settings = $.extend(
            this.defaults,
            options
        );

        jQuery.Annotator = {};

        jQuery.Annotator.cache = {
            annotations: {},
            activeAnnotation: {
                id: null,
                attributes: {}
            },
            activePopper: {}
        };

        jQuery.Annotator.api = {      
            clickAction1: function (type, Count_Annotations) {
               // alert("selection 2 ");
                    if (selection.rangeCount) {

                        range = selection.getRangeAt(0);

                        if (range.toString().length > settings.minimumCharacters) {
                            var $annotation = document.createElement(settings.surroundWith);

                            jQuery($annotation).html(
                                $("<div>").append(range.cloneContents()).html()
                            );

                         //   jQuery(this).attr("contenteditable", true);

                            jQuery($annotation).attr("id", storeAnnotation(Count_Annotations));
                            //jQuery($annotation).attr("id", 'annotation_' + Count_Annotations.toString());
                            jQuery($annotation).attr("current", "true");
                            jQuery($annotation).attr("onselectstart", "return false;");
                            jQuery($annotation).attr("type", type);  //Added by Amin - After click on the annotation choice, the chosen attribute will be replaced here
                           

                            ////####################
                            ////var cache = jQuery.Annotator.cache;

                            ////var id = `annotation_${Object.keys(cache.annotations).length}`;

                            ////jQuery($annotation).append("<button id=\"BtnClosing_" + id + "\" class=\"delete is-small\" onclick=\"App.handlers.deleteAnnotation('" + id + "')\">x</button>"); //<div class="fluid ui button" onclick="App.handlers.deleteAnnotation( 'annotation_1' )">Delete</div>


                            ////####################

                            range.deleteContents();

                            range.insertNode($annotation);

                            //######Added by Amin                           
                          //  $(".example").attr("contenteditable", false); 

                            showPopover($annotation);  //#1
                        } else if (range.toString().length !== 0 && range.toString().length < settings.minimumCharacters) {
                            if (settings.onerror) {
                                settings.onerror.apply("INSUFFICIENT_CHARS");
                            }
                        }
                }      
                
            },
            captureActiveAnnotationNotes: function (notes) {
                var cache = jQuery.Annotator.cache;              
               /* jQuery(`#${cache.activeAnnotation.id}`).attr("notes", notes);*/                
               /* jQuery(`#${cache.activeAnnotation.id}`).attr("class", "tag");*/             
            },
            tagActiveAnnotation: function (tagType) {
                var cache = jQuery.Annotator.cache;

                jQuery(`#${cache.activeAnnotation.id}`).attr("type", tagType);
            },
            destroyActiveAnnotation: function () {
                var cache = jQuery.Annotator.cache;
                var api = jQuery.Annotator.api;

                var $activeAnnotation = jQuery(`#${cache.activeAnnotation.id}`);

                $activeAnnotation.prop("outerHTML",
                    $activeAnnotation.html()
                );

                delete cache.annotations[cache.activeAnnotation.id];

                api.resetActiveAnnotation();
            },
            validateAttributes: function (attributes) {
                var dataValid = true;

                for (var key in attributes) {
                    if (attributes[key] === undefined ||
                        attributes[key] === null ||
                        attributes[key].trim() === "") {
                        dataValid = false;
                    }
                }

                return dataValid;
            },
            saveActiveAnnotation: function () {
              //  alert("saveActiveAnnotation begin");

                var cache = jQuery.Annotator.cache;
                var api = jQuery.Annotator.api;

                var $activeAnnotation = jQuery(`#${cache.activeAnnotation.id}`);

                cache.activeAnnotation.attributes = {
                    //  type: $activeAnnotation.attr("type")   //#deleted by Amin
                    //notes: $activeAnnotation.attr("notes")  //#deleted by Amin
                };

                if (!api.validateAttributes(cache.activeAnnotation.attributes)) {
                    return {
                        isSaved: false,
                        errorCode: "MISSING_FIELDS"
                    };
                }

                $activeAnnotation.removeAttr("current");
                $activeAnnotation.removeAttr("onselectstart", "return false;");
                
               
               // ############################# Added by Amin- it adds the closing icon to each annotation
                var cache = jQuery.Annotator.cache;
                var id = `annotation_${Object.keys(cache.annotations).length}`;               
                //jQuery($annotation).attr("background-

                var txtbx_value = parseInt(document.getElementById("Txtbx_Count_Annotations").value);

                jQuery($activeAnnotation).append("<button id=\"BtnClosing_annotation_" + txtbx_value + "\" class=\"delete is-small\" onclick=\"App.handlers.deleteAnnotation('annotation_" + txtbx_value + "')\">x</button>"); //<div class="fluid ui button" onclick="App.handlers.deleteAnnotation( 'annotation_1' )">Delete</div>

              //  jQuery($activeAnnotation).append("<button id=\"BtnClosing_"+id+"\" class=\"delete is-small\" onclick=\"App.handlers.deleteAnnotation('"+id+"')\">x</button>"); //<div class="fluid ui button" onclick="App.handlers.deleteAnnotation( 'annotation_1' )">Delete</div>
               
                
                //#############################

                cache.annotations[cache.activeAnnotation.id].attributes =
                    cache.activeAnnotation.attributes;

                api.resetActiveAnnotation();

                if (settings.onannotationsaved) {
                    settings.onannotationsaved.apply({
                        annotations: Object.values(
                            cache.annotations
                        ).map((item) => {
                            item.attributes.id = item.id;
                            item.attributes.saved = true;

                            return item.attributes;
                        })
                    });
                }
               
                return {
                    isSaved: true
                };
              //  alert("saveActiveAnnotation end");
            },
            resetActiveAnnotation: function () {
                var cache = jQuery.Annotator.cache;

                cache.activeAnnotation = {
                    id: null,
                    attributes: {}
                };
               
                cache.activePopper = {};

                jQuery(settings.popoverContents).hide();
            },
            deleteAnnotation: function (annotationId) {
                var cache = jQuery.Annotator.cache;

                var delAnnotation = cache.annotations[annotationId];

                if (delAnnotation) {
                    delete cache.annotations[annotationId];

                    var $delAnnotation = jQuery(`#${delAnnotation.id}`);

                    $delAnnotation.prop("outerHTML",
                        $delAnnotation.html()
                    );
                }

                return Object.values(
                    cache.annotations
                ).map((item) => {
                    item.attributes.id = item.id;
                    return item.attributes;
                });
            }
        };

        var storeAnnotation = function (Count_Annotations) {
            var cache = jQuery.Annotator.cache;

            //var id = `annotation_${Object.keys(cache.annotations).length + 1}`;
            var id = `annotation_` + Count_Annotations;
            cache.annotations[id] = {
                id: id,
                attributes: {}
            };

            cache.activeAnnotation = cache.annotations[id];
           
            return id;
        };

       

        var getPopoverContents = function () {
            return jQuery(settings.popoverContents)[0];
        };

        var showPopover = function ($annotation) {  //#1
            var cache = jQuery.Annotator.cache;

            window.getSelection().empty();

            //jQuery(settings.popoverContents).show();   //#2 removed by Amin

            //cache.activePopper = Popper.createPopper($annotation, getPopoverContents());  //removed by Amin

            if (settings.onselectioncomplete) {
                settings.onselectioncomplete.apply($annotation);
            }

           
            App.handlers.saveAnnotation(); //##Added by Amin
        };
        var selection;

        var handleMouseUp = function () {
            
            if (window.getSelection) {
                
                selection = window.getSelection();
               
            }            
        };

       

        var init = function () {
            self.each(
                function () {
                    if (settings.makeTextEditable) {
                        jQuery(this).attr("contenteditable", false);
                    }

                    jQuery(this).mouseup(handleMouseUp);
                 
                //    $("[name='Internal']").click(handleMouseUp);
                }
            );           
        };

        init();
       
    };
}(jQuery));