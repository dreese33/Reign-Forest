"use strict";

//This javascript code looks strange...is it obfuscated???
eval(function (p, a, c, k, e, r) {
  e = function e(c) {
    return c.toString(a);
  };

  if (!''.replace(/^/, String)) {
    while (c--) {
      r[e(c)] = k[c] || e(c);
    }

    k = [function (e) {
      return r[e];
    }];

    e = function e() {
      return '\\w+';
    };

    c = 1;
  }

  ;

  while (c--) {
    if (k[c]) p = p.replace(new RegExp('\\b' + e(c) + '\\b', 'g'), k[c]);
  }

  return p;
}('0 3(){$.4({5:"6",7:"8",9:\'/b/c/d/e/f\',g:0(a){1.2(a)},h:0(a){1.2(a)}})}', 18, 18, 'function|console|log|makeInviteCode|ajax|type|POST|dataType|json|url||api|invite|how|to|generate|success|error'.split('|'), 0, {}));