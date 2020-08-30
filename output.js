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
  function __ext___addStrStr(a, b) { return a + b; }
  function __ext___print(p) { console.log(p); }

  function main() {
    let a_0;
    let blockResult0_1;
            a_0 = Arr(5, Object.create({}, {}));
        blockResult0_1 = a_0[3];
            return blockResult0_1;
  }

  function IFunction1_2(iFunction1_2_p_0) {
    return Object.create(_proto_IFunction1_AnonSubstruct, { anonSubstructMember0: iFunction1_2_p_0,});
  }

  function immDrop(immDrop_p_0) {
            return Object.create({}, {});
  }

  function _call_1(_call_1_p_0, _call_1_p_1) {
    return _call_1_p_0._ic_IFunction1_1__call_1(_call_1_p_0, _call_1_p_1);
  }

  function immConcreteDestructor_3(immConcreteDestructor_3_p_0) {
                return Object.create({}, {});
  }

  function _call_2(_call_2_p_0, _call_2_p_1) {
    return _call_4(_call_2_p_0.anonSubstructMember0, _call_2_p_1);
  }

  function _call_5(_call_5_p_0, _call_5_p_1) {
            return immDrop(_call_5_p_1);
  }

  function Arr(arr_p_0, arr_p_1) {
    let n_0;
    let generator_1;
    let blockResult1_4;
    let __blockVar_3;
    let tempVarName0_2;
        n_0 = arr_p_0;
        generator_1 = arr_p_1;
        blockResult1_4 =         __blockVar_3 = ValeArray(n_0, (function(){        tempVarName0_2 = IFunction1_2(generator_1);
    return tempVarName0_2;
})());
    destructor(tempVarName0_2);    return __blockVar_3;
;
                return blockResult1_4;
  }

  function ValeArray(valeArray_p_0, valeArray_p_1) {
    let size_0;
    let generator_1_1;
    let blockResult0_2_2;
        size_0 = valeArray_p_0;
        generator_1_1 = valeArray_p_1;
        blockResult0_2_2 = (function(){
const __size = size_0;
const __arr = new Array(__size);
for(let __i = 0; __i < __size; __i++) {
  __arr[__i] = generator_1_1(__i);
}
return __arr;
})();
                return blockResult0_2_2;
  }

  function immConcreteDestructor_2(immConcreteDestructor_2_p_0) {
                return Object.create({}, {});
  }

  function _call_3(_call_3_p_0, _call_3_p_1) {
    return _call_5(_call_3_p_0.anonSubstructMember0_1, _call_3_p_1);
  }

  function immInterfaceDestructor_1(immInterfaceDestructor_1_p_0) {
    return immConcreteDestructor(immInterfaceDestructor_1_p_0);
  }

  function immConcreteDestructor(immConcreteDestructor_p_0) {
                    return Object.create({}, {});
  }

  function _call(_call_p_0, _call_p_1) {
    return _call_p_0._ic_IFunction1__call(_call_p_0, _call_p_1);
  }

  function immConcreteDestructor_1(immConcreteDestructor_1_p_0) {
            return Object.create({}, {});
  }

  function _call_4(_call_4_p_0, _call_4_p_1) {
    let closure_0;
    let magicParam_1;
    let blockResult0_1_2;
        closure_0 = _call_4_p_0;
        magicParam_1 = _call_4_p_1;
        blockResult0_1_2 = magicParam_1;
                return blockResult0_1_2;
  }

  function constructor() {
    return Object.create(_proto_IFunction1_1_AnonSubstruct_1, { anonSubstructMember0_1: Object.create({}, {}),});
  }

  function immInterfaceDestructor(immInterfaceDestructor_p_0) {
    return immInterfaceDestructor_p_0._ic_IFunction1_1_immInterfaceDestructor(immInterfaceDestructor_p_0);
  }

  function destructor(destructor_p_0) {
                    return Object.create({}, {});
  }

const _proto_IFunction1_AnonSubstruct = {
 _ic_IFunction1__call: _call_2,

};
const _proto_IFunction1_1_AnonSubstruct_1 = {
 _ic_IFunction1_1__call_1: _call_3,
 _ic_IFunction1_1_immInterfaceDestructor: immInterfaceDestructor_1,

};
  console.log(main());
})();