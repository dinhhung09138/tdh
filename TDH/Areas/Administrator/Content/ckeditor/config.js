/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    //config.plugins = 'dialogui,dialog,about,a11yhelp,dialogadvtab,basicstyles,bidi,blockquote,notification,button,toolbar,clipboard,panelbutton,panel,floatpanel,colorbutton,colordialog,menu,contextmenu,copyformatting,div,resize,elementspath,enterkey,entities,popup,filebrowser,find,fakeobjects,flash,floatingspace,listblock,richcombo,font,forms,format,horizontalrule,htmlwriter,iframe,wysiwygarea,image,indent,indentblock,indentlist,smiley,justify,menubutton,language,link,list,liststyle,magicline,maximize,pagebreak,pastetext,pastefromword,removeformat,selectall,showblocks,showborders,sourcearea,specialchar,scayt,stylescombo,tab,table,tabletools,tableselection,undo,lineutils,widgetselection,widget,filetools,notificationaggregator,uploadwidget,uploadimage,wsc,pastefromexcel,uicolor,brclear,googledocs,gg,imagepaste,imageresize,imagebrowser,imageresizerowandcolumn,imagerotate,simage,imageuploader,toc,contents,tableresize,tableresizerowandcolumn,tabletoolstoolbar,video,videosnapshot,youtube,videodetector,zoom';
    config.plugins = 'dialogui,dialog,about,a11yhelp,dialogadvtab,basicstyles,bidi,blockquote,notification,button,toolbar,clipboard,panelbutton,panel,floatpanel,colorbutton,colordialog,menu,contextmenu,copyformatting,div,resize,elementspath,enterkey,entities,popup,filebrowser,find,fakeobjects,floatingspace,listblock,richcombo,font,forms,format,horizontalrule,htmlwriter,iframe,wysiwygarea,image,indent,indentblock,indentlist,smiley,justify,menubutton,link,list,liststyle,magicline,maximize,pagebreak,pastetext,pastefromword,removeformat,showblocks,showborders,sourcearea,specialchar,scayt,stylescombo,tab,table,undo,lineutils,widgetselection,widget,filetools,notificationaggregator,wsc,pastefromexcel,uicolor,brclear,googledocs,gg,imagepaste,imageresize,imagebrowser,imageresizerowandcolumn,imagerotate,simage,toc,contents,tableresize,tableresizerowandcolumn,tabletoolstoolbar,videosnapshot,youtube';
    
    config.defaultLanguage = 'vi';
    config.language = 'vi';
    //config.uiColor = '#AADC6E';
    config.filebrowserBrowseUrl = "/Areas/Admin/Content/ckfinder/ckfinder.html";
    config.filebrowserImageUrl = "/Areas/Admin/Content/ckfinder/ckfinder.html?type=Images";
    config.filebrowserFlashUrl = "/Areas/Admin/Content/ckfinder/ckfinder.html?type=Flash";
    config.filebrowserUploadUrl = "/Areas/Admin/Content/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files";
    config.filebrowserImageUploadUrl = "/Areas/Admin/Content/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images";
    config.filebrowserFlashUploadUrl = "/Areas/Admin/Content/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash";
    //
    // CKEDITOR PLUGINS LOADING
    config.extraPlugins = 'pbckcode'; // add other plugins here (comma separated)
    
    // ADVANCED CONTENT FILTER (ACF)
    // ACF protects your CKEditor instance of adding unofficial tags
    // however it strips out the pre tag of pbckcode plugin
    // add this rule to enable it, useful when you want to re edit a post

    // PBCKCODE CUSTOMIZATION
    config.pbckcode = {
        // An optional class to your pre tag.
        cls: '',

        // The syntax highlighter you will use in the output view
        highlighter: 'PRETTIFY',

        // An array of the available modes for you plugin.
        // The key corresponds to the string shown in the select tag.
        // The value correspond to the loaded file for ACE Editor.
        modes: [
            ['HTML', 'html'],
            ['CSS', 'css'],
            ['JavaScript', 'javascript'],
            ['C#', 'csharp'],
            ['PHP', 'php'],
            ['Markdown', 'markdown'],
            ['Python', 'python'],
            ['R', 'ruby'],
            ['SQL', 'sql'],
            ['XML', 'xml']
        ],

        // The theme of the ACE Editor of the plugin.
        theme: 'textmate',

        // Tab indentation (in spaces)
        tab_size: '4'
    };
};
