// Leetcode Topic: Strings

/**
 * Valid Parenthesis
 *
 * @param {string} s
 * @return {boolean}
 */

const opening = ["{", "[", "("];
const closing = ["}", "]", ")"];

var isValid = function (s) {
    let stack = [];

    for (let i = 0; i < s.length; i++) {
        let c = s[i];
        if (opening.includes(c)) {
            stack.push(c);
        }

        if (closing.includes(c)) {
            if (stack.length === 0) return false;
            if (opening.indexOf(stack.pop()) !== closing.indexOf(c))
                return false;
        }
    }

    return stack.length === 0 ? true : false;
};

/**
 * Valid Palindrome 2
 * @param {string} s
 * @return {boolean}
 */
var validPalindrome = function (s) {
    for (let i = 0, stop = s.length / 2; i < stop; i++) {
        let j = s.length - i - 1;
        if (s[i] !== s[j]) {
            return isPalindrome(cut(s, i)) || isPalindrome(cut(s, j));
        }
    }
    return true;
};

const cut = (s, i) => s.substr(0, i) + s.substr(i + 1);

const isPalindrome = (s) => s === s.split("").reverse().join("");
