mergeInto(LibraryManager.library, {
    OpenLinkOnDevice: function (urlPtr) {
        // Convert the C# string pointer to a JavaScript string
        var url = UTF8ToString(urlPtr);

        // Detect the device type using your existing detection logic
        if (typeof navigator !== "undefined") {
            if (/iPhone/.test(navigator.userAgent)) {
                console.log("Detected iPhone, opening link...");
                setTimeout(function () {
                    window.location.href = url; // Redirect directly to URL for iPhone
                }, 0);
            } else if (/iPad/.test(navigator.userAgent)) {
                console.log("Detected iPad, opening link...");
                setTimeout(function () {
                    window.location.href = url; // Redirect directly to URL for iPad
                }, 0);
            } else {
                console.log("Neither iPhone nor iPad detected, link not opened.");
            }
        } else {
            console.log("Navigator is undefined, unable to detect device.");
        }
    }
});
