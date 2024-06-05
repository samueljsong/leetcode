//daily
//Start javascript

var lengthOfLongestSubstring = function (s) {
    let l = 0;
    let max = 0;
    let substring = new Set();
    for (let i = 0; i < s.length; i++) {
        while (substring.has(s[i])) {
            substring.delete(s[l]);
            l++;
        }

        substring.add(s[i]);
        max = Math.max(max, substring.size);
    }
    return max;
};

var checkInclusion = function (s1, s2) {
    let requiredChar = s1.length; // reaches 0 -> return true
    let hashMap = new Map();

    s1.split("").forEach((char) => {
        hashMap[char] = (hashMap[char] ?? 0) + 1;
    });

    let l = 0;

    for (let r = 0; r < s2.length; ) {
        if (hashMap[s2[r]] > 0) {
            requiredChar--;
        }
        hashMap[s2[r]]--;
        r++;

        if (requiredChar === 0) {
            return true;
        }

        if (r - l === s1.length) {
            if (hashMap[s2[l]] >= 0) {
                requiredChar++;
            }
            hashMap[s2[l]]++;
            l++;
        }
    }

    return false;
};

var characterReplacement = function (s, k) {
    const hashMap = new Map();
    let l = 0,
        freq = 0,
        longest = 0;

    for (let r = 0; r < s.length; r++) {
        hashMap[s[r]] = (hashMap[s[r]] ?? 0) + 1;

        freq = Math.max(freq, hashMap[s[r]]);

        if (freq + k < r - l + 1) {
            hashMap[s[l]] = hashMap[s[l]] - 1;
            l++;
        }

        longest = Math.max(longest, r - l + 1);
    }
    return longest;
};

var minWindow = function (s, t) {
    let neededChar = new Map();
    t.split("").forEach((c) => {
        neededChar[c] = (neededChar[c] ?? 0) + 1;
    });

    let l = 0;
    let r = 0;

    let requiredLength = t.length;
    let minLength = s.length;

    while (r < s.length) {
        if (neededChar[s[r]] > 0) {
            neededChar[s[r]] = neededChar[s[r]] - 1;
            requiredLength -= 1;
        }
        r++;

        if (requiredLength === 0) {
        }
    }

    console.log(neededChar);
};
