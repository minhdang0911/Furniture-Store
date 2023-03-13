/**
 * @license Copyright (c) 2003-2019, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.syntaxhighlight_lang = 'csharp';
    config.syntaxhighlight_hideControls = true;
    config.language ='vi';

    config.filebrowserBrowseUrl = '/Content/js/Plugins/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl ='/Content/js/Plugins/ckfinder/html?Type=Images';
    config.filebrowserFlashBrowseUrl = '/Content/js/Plugins/ckfinder/html?Type=Flash';
    config.filebrowserUploadUrl ='/Content/js/Plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl ='/DataImage';
    config.filebrowserFlashUploadUrl = '/Content/js/Plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
    CKFinder.SetupCKEditor(null, '/Content/js/Plugins/ckfinder/');
};
