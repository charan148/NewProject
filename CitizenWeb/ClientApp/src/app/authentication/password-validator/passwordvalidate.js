"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var PasswordValidation = /** @class */ (function () {
    function PasswordValidation() {
    }
    PasswordValidation.MatchPassword = function (AC) {
        var password = AC.get('Password').value;
        if (AC.get('ConfirmPassword').touched || AC.get('ConfirmPassword').dirty) {
            var verifyPassword = AC.get('ConfirmPassword').value;
            if (password != verifyPassword) {
                AC.get('ConfirmPassword').setErrors({ MatchPassword: true });
            }
            else {
                return null;
            }
        }
    };
    return PasswordValidation;
}());
exports.PasswordValidation = PasswordValidation;
//# sourceMappingURL=passwordvalidate.js.map