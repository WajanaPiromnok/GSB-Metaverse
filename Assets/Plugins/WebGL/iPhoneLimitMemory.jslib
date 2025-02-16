mergeInto(LibraryManager.library, {
    SetMemorySize: function (sizeInMB) {
        var TOTAL_MEMORY = sizeInMB * 1024 * 1024; // Convert MB to bytes
        console.log("Memory size set to: " + sizeInMB + " MB");
    }
});
