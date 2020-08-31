'use strict';
(function(){
  function __ext___addIntInt(a, b) { return ((a|0) + (b|0)) | 0; }
  function __ext___subtractIntInt(a, b) { return ((a|0) - (b|0)) | 0; }
  function __ext___multiplyIntInt(a, b) { return Math.imul(a|0, b|0) | 0; }
  function __ext___lessThanInt(a, b) { return (a|0) < (b|0); }
  function __ext___lessThanIntOrEqInt(a, b) { return (a|0) <= (b|0); }
  function __ext___greaterThanInt(a, b) { return (a|0) > (b|0); }
  function __ext___greaterThanOrEqInt(a, b) { return (a|0) >= (b|0); }
  function __ext___eqIntInt(a, b) { return (a|0) === (b|0); }
  function __ext___and(a, b) { return !!a && !!b; }
  function __ext___addStrStr(a, b) { return a + b; }
  function __ext___print(p) { console.log(p); }

  function __addIntInt(__addIntInt_p_0, __addIntInt_p_1) {
    return __ext___addIntInt(__addIntInt_p_0, __addIntInt_p_1);
  }

  function main() {
    let i_0;
    let blockResult0_1;
            i_0 = 0;
    while ((($60_(i_0, 10)) ? function(){
            print('hi');            return Object.create({}, {});
      return true;

} : function(){
return false;
})()
) {}
        blockResult0_1 = 0;
            return blockResult0_1;
  }

  function $60_($60__p_0, $60__p_1) {
    let left_0;
    let right_1;
    let blockResult0_3_2;
        left_0 = $60__p_0;
        right_1 = $60__p_1;
        blockResult0_3_2 = __lessThanInt(left_0, right_1);
                return blockResult0_3_2;
  }

  function $43_($43__p_0, $43__p_1) {
    let a_0;
    let b_1;
    let blockResult0_1_2;
        a_0 = $43__p_0;
        b_1 = $43__p_1;
        blockResult0_1_2 = __addIntInt(a_0, b_1);
                return blockResult0_1_2;
  }

  function __lessThanInt(__lessThanInt_p_0, __lessThanInt_p_1) {
    return __ext___lessThanInt(__lessThanInt_p_0, __lessThanInt_p_1);
  }

  function __print(__print_p_0) {
    return __ext___print(__print_p_0);
  }

  function print(print_p_0) {
    let output_0;
    let blockResult0_2_1;
        output_0 = print_p_0;
        blockResult0_2_1 = __print(output_0);
            return blockResult0_2_1;
  }

  console.log(main());
})();