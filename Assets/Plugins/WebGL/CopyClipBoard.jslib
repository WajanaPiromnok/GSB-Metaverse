mergeInto(LibraryManager.library, {

  CopyText: function (str) {
    //window.alert(Pointer_stringify(str));
    //openLink();
	
	var parsedStr = UTF8ToString(str);
	
		/* Copy the text inside the text field */
	  navigator.clipboard.writeText(parsedStr);

	  /* Alert the copied text */
	  //alert("Copied the text: " + parsedStr);
  
  },

  PrintFloatArray: function (array, size) {
    for(var i = 0; i < size; i++)
    console.log(HEAPF32[(array >> 2) + i]);
  },

  AddNumbers: function (x, y) {
    return x + y;
  },

  StringReturnValueFunction: function () {
    var returnStr = "bla";
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  },

  BindWebGLTexture: function (texture) {
    GLctx.bindTexture(GLctx.TEXTURE_2D, GL.textures[texture]);
  },

});