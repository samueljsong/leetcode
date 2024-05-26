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
