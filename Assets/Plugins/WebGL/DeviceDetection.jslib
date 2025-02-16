mergeInto(LibraryManager.library, {
    IsIPhone: function () {
        // Check if the user agent contains "iPhone"
        if (typeof navigator !== "undefined" && /iPhone/.test(navigator.userAgent)) {
            return 1; // True for iPhone
        }
        return 0; // False
    },
    IsIPad: function () {
        // Check if the user agent contains "iPad"
        if (typeof navigator !== "undefined" && /iPad/.test(navigator.userAgent)) {
            return 1; // True for iPad
        }
        return 0; // False
    }
});
