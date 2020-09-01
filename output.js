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
  function __ext___eqStrStr(a, b) { return a === b; }
  function __ext___and(a, b) { return !!a && !!b; }
  function __ext___not(a) { return !a; }
  function __ext___print(p) { console.log(p); }
  function __ext___getch() { if (typeof window == 'undefined') { return 5; } while (true) { const result = window.prompt('Press a key'); if (typeof result === 'string' && result.length !== 0) { return result.charCodeAt(0); } } }
  function makeBoard() {
    let funcResult_0;
return (      funcResult_0 = Arr(10, Object.create({}, {})), funcResult_0)  }

  function immConcreteDestructor_1(immConcreteDestructor_1_p_0) {
return ((immConcreteDestructor_1_p_0), void(0), void(0))  }

  function __addIntInt(__addIntInt_p_0, __addIntInt_p_1) {
return __ext___addIntInt(__addIntInt_p_0, __addIntInt_p_1)  }

  function pause() {
    let i_1_0;
    let blockResult0_3_1;
return ((    i_1_0 = 0), (function(){ while ((($60_(i_1_0, 4000000)) ? function(){
return ((      i_1_0 = $43_(i_1_0, 1), void(0)), true)
} : function(){
return false;
})()
) {} })()
,     blockResult0_3_1 = void(0), void(0), blockResult0_3_1)  }

  function len_2(len_2_p_0) {
    let arr_4_0;
    let blockResult0_10_1;
return (    arr_4_0 = len_2_p_0,     blockResult0_10_1 = arr_4_0.length, void(0), blockResult0_10_1)  }

  function main() {
    let board_1_0;
    let playerRow_3_1;
    let playerCol_3_2;
    let directions_3;
    let blockResult0_4;
    let __blockVar_5;
    let __blockVar_6;
return ((    board_1_0 = makeBoard()), (    playerRow_3_1 = Object.create({}, { __boxee_1: { writable: true, value: 4, },})), (    playerCol_3_2 = Object.create({}, { __boxee_1: { writable: true, value: 3, },})), display(board_1_0, playerRow_3_1.__boxee_1, playerCol_3_2.__boxee_1), (    directions_3 = ['up', 'right', 'up', 'right', 'down', 'left', 'up', 'right', 'down', 'left', 'up', 'right', 'down', 'right', 'down', 'left', 'up', 'right', 'down', 'left', 'up', 'left', 'up', 'right', 'down', 'left', 'up']), each(directions_3, Object.create({}, { playerRow_2: { writable: true, value: playerRow_3_1, }, playerCol_2: { writable: true, value: playerCol_3_2, }, board: { writable: true, value: board_1_0, },})),     blockResult0_4 = 0, void(0),     (playerCol_3_2, __blockVar_5),     (playerRow_3_1, __blockVar_6), destructor(board_1_0), blockResult0_4)  }

  function IFunction1_4(iFunction1_4_p_0) {
return Object.create(_proto_IFunction1_2_AnonSubstruct_2, { anonSubstructMember0_2: { writable: true, value: iFunction1_4_p_0, },})  }

  function each(each_p_0, each_p_1) {
    let arr_0;
    let func_1;
    let i_2;
    let l_3;
    let blockResult0_1_4;
return (    arr_0 = each_p_0,     func_1 = each_p_1, (    i_2 = 0), (    l_3 = len(arr_0)), (function(){ while ((($60_(i_2, l_3)) ? function(){
return ((__call_8(func_1, arr_0[i_2]),       i_2 = $43_(i_2, 1), void(0)), true)
} : function(){
return false;
})()
) {} })()
,     blockResult0_1_4 = void(0), void(0), void(0), destructor_1(func_1), void(0), blockResult0_1_4)  }

  function drop(drop_p_0) {
return (destructor_4(drop_p_0), void(0))  }

  function __call_4(__call_4_p_0, __call_4_p_1) {
return __call_12(__call_4_p_0.anonSubstructMember0, __call_4_p_1)  }

  function eachI_1(eachI_1_p_0, eachI_1_p_1) {
    let arr_3_0;
    let func_2_1;
    let i_3_2;
    let l_2_3;
    let blockResult0_8_4;
return (    arr_3_0 = eachI_1_p_0,     func_2_1 = eachI_1_p_1, (    i_3_2 = 0), (    l_2_3 = len_2(arr_3_0)), (function(){ while ((($60_(i_3_2, l_2_3)) ? function(){
return ((__call_10(func_2_1, i_3_2, arr_3_0[i_3_2]),       i_3_2 = $43_(i_3_2, 1), void(0)), true)
} : function(){
return false;
})()
) {} })()
,     blockResult0_8_4 = void(0), void(0), void(0), void(0), void(0), blockResult0_8_4)  }

  function __eqIntInt(__eqIntInt_p_0, __eqIntInt_p_1) {
return __ext___eqIntInt(__eqIntInt_p_0, __eqIntInt_p_1)  }

  function __call_3(__call_3_p_0, __call_3_p_1) {
return __call_3_p_0._ic_IFunction1_3___call_3(__call_3_p_0, __call_3_p_1)  }

  function immDrop(immDrop_p_0) {
return (void(0), void(0))  }

  function immConcreteDestructor_4(immConcreteDestructor_4_p_0) {
return ((immConcreteDestructor_4_p_0, void(0)), void(0), void(0), void(0))  }

  function IFunction1_5(iFunction1_5_p_0) {
return Object.create(_proto_IFunction1_AnonSubstruct, { anonSubstructMember0: { writable: true, value: iFunction1_5_p_0, },})  }

  function __call_11(__call_11_p_0, __call_11_p_1) {
    let closure_3_0;
    let row_2_1;
    let blockResult0_12_2;
return (    closure_3_0 = __call_11_p_0,     row_2_1 = __call_11_p_1,     blockResult0_12_2 = Arr_1(10, Object.create({}, { row: { writable: true, value: row_2_1, },})), void(0), void(0), blockResult0_12_2)  }

  function eachI(eachI_p_0, eachI_p_1) {
    let arr_2_0;
    let func_1_1;
    let i_2_2;
    let l_1_3;
    let blockResult0_6_4;
return (    arr_2_0 = eachI_p_0,     func_1_1 = eachI_p_1, (    i_2_2 = 0), (    l_1_3 = len_1(arr_2_0)), (function(){ while ((($60_(i_2_2, l_1_3)) ? function(){
return ((__call_9(func_1_1, i_2_2, arr_2_0[i_2_2]),       i_2_2 = $43_(i_2_2, 1), void(0)), true)
} : function(){
return false;
})()
) {} })()
,     blockResult0_6_4 = void(0), void(0), void(0), void(0), void(0), blockResult0_6_4)  }

  function constructor() {
return Object.create(_proto_IFunction1_1_AnonSubstruct_1, { anonSubstructMember0_1: { writable: true, value: Object.create({}, {}), },})  }

  function immConcreteDestructor(immConcreteDestructor_p_0) {
return ((immConcreteDestructor_p_0), void(0), void(0))  }

  function immConcreteDestructor_8(immConcreteDestructor_8_p_0) {
return ((immConcreteDestructor_8_p_0), void(0))  }

  function __call_1(__call_1_p_0, __call_1_p_1) {
return __call_1_p_0._ic_IFunction1_1___call_1(__call_1_p_0, __call_1_p_1)  }

  function $60_($60__p_0, $60__p_1) {
    let left_2_0;
    let right_2_1;
    let blockResult0_19_2;
return (    left_2_0 = $60__p_0,     right_2_1 = $60__p_1,     blockResult0_19_2 = __lessThanInt(left_2_0, right_2_1), void(0), void(0), blockResult0_19_2)  }

  function and(and_p_0, and_p_1) {
    let left_1_0;
    let right_1_1;
    let blockResult0_18_2;
return (    left_1_0 = and_p_0,     right_1_1 = and_p_1,     blockResult0_18_2 = __and(left_1_0, right_1_1), void(0), void(0), blockResult0_18_2)  }

  function constructor_1() {
return Object.create(_proto_IFunction1_3_AnonSubstruct_3, { anonSubstructMember0_3: { writable: true, value: Object.create({}, {}), },})  }

  function $61_$61_($61_$61__p_0, $61_$61__p_1) {
    let left_3_0;
    let right_3_1;
    let blockResult0_22_2;
return (    left_3_0 = $61_$61__p_0,     right_3_1 = $61_$61__p_1,     blockResult0_22_2 = __eqStrStr(left_3_0, right_3_1), void(0), void(0), blockResult0_22_2)  }

  function immConcreteDestructor_7(immConcreteDestructor_7_p_0) {
return ((immConcreteDestructor_7_p_0), void(0), void(0))  }

  function __call_9(__call_9_p_0, __call_9_p_1, __call_9_p_2) {
    let closure_1_0;
    let rowI_1_1;
    let row_1_2;
    let blockResult0_7_3;
return (    closure_1_0 = __call_9_p_0,     rowI_1_1 = __call_9_p_1,     row_1_2 = __call_9_p_2, eachI_1(row_1_2, Object.create({}, { rowI: { writable: true, value: rowI_1_1, }, playerRow_1: { writable: true, value: closure_1_0.playerRow, }, playerCol_1: { writable: true, value: closure_1_0.playerCol, }, toPrint_1: { writable: true, value: closure_1_0.toPrint, },})),     closure_1_0.toPrint.__boxee = $43__1(closure_1_0.toPrint.__boxee, '\n'),     blockResult0_7_3 = void(0), void(0), void(0), void(0), blockResult0_7_3)  }

  function immConcreteDestructor_3(immConcreteDestructor_3_p_0) {
return ((immConcreteDestructor_3_p_0, void(0)), void(0), void(0), void(0), void(0))  }

  function __call_2(__call_2_p_0, __call_2_p_1) {
return __call_2_p_0._ic_IFunction1_2___call_2(__call_2_p_0, __call_2_p_1)  }

  function immInterfaceDestructor_3(immInterfaceDestructor_3_p_0) {
return immConcreteDestructor_1(immInterfaceDestructor_3_p_0)  }

  function $43__1($43__1_p_0, $43__1_p_1) {
    let a_0;
    let b_1;
    let blockResult0_16_2;
return (    a_0 = $43__1_p_0,     b_1 = $43__1_p_1,     blockResult0_16_2 = __addStrStr(a_0, b_1), void(0), void(0), blockResult0_16_2)  }

  function __call_10(__call_10_p_0, __call_10_p_1, __call_10_p_2) {
    let closure_2_0;
    let cellI_1;
    let cell_2;
    let charToPrint_3;
    let blockResult0_9_4;
return (    closure_2_0 = __call_10_p_0,     cellI_1 = __call_10_p_1,     cell_2 = __call_10_p_2, (    charToPrint_3 = cell_2), ((and($61_$61__1(closure_2_0.rowI, closure_2_0.playerRow_1), $61_$61__1(cellI_1, closure_2_0.playerCol_1))) ? function(){
return (      charToPrint_3 = '@', void(0))
} : function(){
return void(0);
})()
,     closure_2_0.toPrint_1.__boxee = $43__1(closure_2_0.toPrint_1.__boxee, charToPrint_3),     blockResult0_9_4 = void(0), void(0), void(0), void(0), void(0), blockResult0_9_4)  }

  function __call_13(__call_13_p_0, __call_13_p_1) {
return (void(0), drop(__call_13_p_1))  }

  function destructor_3(destructor_3_p_0) {
return ((destructor_3_p_0), void(0), void(0))  }

  function __call_5(__call_5_p_0, __call_5_p_1) {
return __call_14(__call_5_p_0.anonSubstructMember0_1, __call_5_p_1)  }

  function Arr(arr_p_0, arr_p_1) {
    let n_0;
    let generator_1;
    let blockResult1_4;
    let __blockVar_3;
    let tempVarName0_2;
return (    n_0 = arr_p_0,     generator_1 = arr_p_1,     blockResult1_4 = (    __blockVar_3 = ValeArray(n_0, (    tempVarName0_2 = IFunction1_4(generator_1), tempVarName0_2)), destructor_2(tempVarName0_2), __blockVar_3), void(0), void(0), blockResult1_4)  }

  function $45_($45__p_0, $45__p_1) {
    let left_0;
    let right_1;
    let blockResult0_14_2;
return (    left_0 = $45__p_0,     right_1 = $45__p_1,     blockResult0_14_2 = __subtractIntInt(left_0, right_1), void(0), void(0), blockResult0_14_2)  }

  function __eqStrStr(__eqStrStr_p_0, __eqStrStr_p_1) {
return __ext___eqStrStr(__eqStrStr_p_0, __eqStrStr_p_1)  }

  function immConcreteDestructor_6(immConcreteDestructor_6_p_0) {
return ((immConcreteDestructor_6_p_0), void(0))  }

  function destructor_2(destructor_2_p_0) {
return ((destructor_2_p_0), void(0), void(0))  }

  function __call_12(__call_12_p_0, __call_12_p_1) {
    let closure_4_0;
    let col_1;
    let blockResult0_13_2;
return (    closure_4_0 = __call_12_p_0,     col_1 = __call_12_p_1,     blockResult0_13_2 = (($61_$61__1(closure_4_0.row, 0)) ? function(){
return '#';
} : function(){
return (($61_$61__1(col_1, 0)) ? function(){
return '#';
} : function(){
return (($61_$61__1(closure_4_0.row, 9)) ? function(){
return '#';
} : function(){
return (($61_$61__1(col_1, 9)) ? function(){
return '#';
} : function(){
return '.';
})()
;
})()
;
})()
;
})()
, void(0), void(0), blockResult0_13_2)  }

  function $43_($43__p_0, $43__p_1) {
    let a_1_0;
    let b_1_1;
    let blockResult0_17_2;
return (    a_1_0 = $43__p_0,     b_1_1 = $43__p_1,     blockResult0_17_2 = __addIntInt(a_1_0, b_1_1), void(0), void(0), blockResult0_17_2)  }

  function Arr_1(arr_1_p_0, arr_1_p_1) {
    let n_1_0;
    let generator_1_1;
    let blockResult1_1_4;
    let __blockVar_3;
    let tempVarName0_1_2;
return (    n_1_0 = arr_1_p_0,     generator_1_1 = arr_1_p_1,     blockResult1_1_4 = (    __blockVar_3 = Array_1(n_1_0, (    tempVarName0_1_2 = IFunction1_5(generator_1_1), tempVarName0_1_2)), destructor_3(tempVarName0_1_2), __blockVar_3), void(0), void(0), blockResult1_1_4)  }

  function __addStrStr(__addStrStr_p_0, __addStrStr_p_1) {
return __ext___addStrStr(__addStrStr_p_0, __addStrStr_p_1)  }

  function __call_7(__call_7_p_0, __call_7_p_1) {
return __call_13(__call_7_p_0.anonSubstructMember0_3, __call_7_p_1)  }

  function destructor(destructor_p_0) {
return (void(0))  }

  function ValeArray(valeArray_p_0, valeArray_p_1) {
    let size_0;
    let generator_2_1;
    let blockResult0_20_2;
return (    size_0 = valeArray_p_0,     generator_2_1 = valeArray_p_1,     blockResult0_20_2 = (function(){
const __size = size_0;
const __generator = generator_2_1;
const __arr = new Array(__size);
for(let __i = 0; __i < __size; __i++) {
  __arr[__i] = __generator._ic_IFunction1_2___call_2(__generator, __i);
}
return __arr;
})(), void(0), void(0), blockResult0_20_2)  }

  function __lessThanInt(__lessThanInt_p_0, __lessThanInt_p_1) {
return __ext___lessThanInt(__lessThanInt_p_0, __lessThanInt_p_1)  }

  function len(len_p_0) {
    let arr_1_0;
    let blockResult0_4_1;
return (    arr_1_0 = len_p_0,     blockResult0_4_1 = 27, void(0), blockResult0_4_1)  }

  function __call_6(__call_6_p_0, __call_6_p_1) {
return __call_11(__call_6_p_0.anonSubstructMember0_2, __call_6_p_1)  }

  function Array_1(array_1_p_0, array_1_p_1) {
    let size_1_0;
    let generator_3_1;
    let blockResult0_21_2;
return (    size_1_0 = array_1_p_0,     generator_3_1 = array_1_p_1,     blockResult0_21_2 = (function(){
const __size = size_1_0;
const __generator = generator_3_1;
const __arr = new Array(__size);
for(let __i = 0; __i < __size; __i++) {
  __arr[__i] = __generator._ic_IFunction1___call(__generator, __i);
}
return __arr;
})(), void(0), void(0), blockResult0_21_2)  }

  function immConcreteDestructor_2(immConcreteDestructor_2_p_0) {
return (void(0), void(0))  }

  function __print(__print_p_0) {
return __ext___print(__print_p_0)  }

  function destructor_1(destructor_1_p_0) {
return ((destructor_1_p_0, void(0), void(0)), void(0), void(0))  }

  function $61_$61__1($61_$61__1_p_0, $61_$61__1_p_1) {
    let left_4_0;
    let right_4_1;
    let blockResult0_23_2;
return (    left_4_0 = $61_$61__1_p_0,     right_4_1 = $61_$61__1_p_1,     blockResult0_23_2 = __eqIntInt(left_4_0, right_4_1), void(0), void(0), blockResult0_23_2)  }

  function print(print_p_0) {
    let output_0;
    let blockResult0_15_1;
return (    output_0 = print_p_0,     blockResult0_15_1 = __print(output_0), void(0), blockResult0_15_1)  }

  function len_1(len_1_p_0) {
    let arr_5_0;
    let blockResult0_11_1;
return (    arr_5_0 = len_1_p_0,     blockResult0_11_1 = arr_5_0.length, void(0), blockResult0_11_1)  }

  function destructor_4(destructor_4_p_0) {
return (void(0))  }

  function __subtractIntInt(__subtractIntInt_p_0, __subtractIntInt_p_1) {
return __ext___subtractIntInt(__subtractIntInt_p_0, __subtractIntInt_p_1)  }

  function __and(__and_p_0, __and_p_1) {
return __ext___and(__and_p_0, __and_p_1)  }

  function immInterfaceDestructor(immInterfaceDestructor_p_0) {
return immInterfaceDestructor_p_0._ic_IFunction1_1_immInterfaceDestructor(immInterfaceDestructor_p_0)  }

  function __call_8(__call_8_p_0, __call_8_p_1) {
    let closure_0;
    let direction_1;
    let newPlayerRow_2;
    let newPlayerCol_3;
    let blockResult0_2_4;
return (    closure_0 = __call_8_p_0,     direction_1 = __call_8_p_1, pause(), (    newPlayerRow_2 = closure_0.playerRow_2.__boxee_1), (    newPlayerCol_3 = closure_0.playerCol_2.__boxee_1), (($61_$61_(direction_1, 'up')) ? function(){
return (      newPlayerRow_2 = $45_(newPlayerRow_2, 1), void(0))
} : function(){
return (($61_$61_(direction_1, 'down')) ? function(){
return (        newPlayerRow_2 = $43_(newPlayerRow_2, 1), void(0))
} : function(){
return (($61_$61_(direction_1, 'left')) ? function(){
return (          newPlayerCol_3 = $45_(newPlayerCol_3, 1), void(0))
} : function(){
return (($61_$61_(direction_1, 'right')) ? function(){
return (            newPlayerCol_3 = $43_(newPlayerCol_3, 1), void(0))
} : function(){
return void(0);
})()
;
})()
;
})()
;
})()
, (($61_$61_(closure_0.board[newPlayerRow_2][newPlayerCol_3], '.')) ? function(){
return (      closure_0.playerRow_2.__boxee_1 = newPlayerRow_2,       closure_0.playerCol_2.__boxee_1 = newPlayerCol_3, display(closure_0.board, closure_0.playerRow_2.__boxee_1, closure_0.playerCol_2.__boxee_1), void(0))
} : function(){
return void(0);
})()
,     blockResult0_2_4 = void(0), void(0), void(0), void(0), void(0), blockResult0_2_4)  }

  function __call(__call_p_0, __call_p_1) {
return __call_p_0._ic_IFunction1___call(__call_p_0, __call_p_1)  }

  function immInterfaceDestructor_1(immInterfaceDestructor_1_p_0) {
return immInterfaceDestructor_1_p_0._ic_IFunction1_3_immInterfaceDestructor_1(immInterfaceDestructor_1_p_0)  }

  function display(display_p_0, display_p_1, display_p_2) {
    let board_2_0;
    let playerRow_4_1;
    let playerCol_4_2;
    let toPrint_2_3;
    let blockResult0_5_4;
    let __blockVar_5;
return (    board_2_0 = display_p_0,     playerRow_4_1 = display_p_1,     playerCol_4_2 = display_p_2, (    toPrint_2_3 = Object.create({}, { __boxee: { writable: true, value: '\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n', },})), eachI(board_2_0, Object.create({}, { toPrint: { writable: true, value: toPrint_2_3, }, playerRow: { writable: true, value: playerRow_4_1, }, playerCol: { writable: true, value: playerCol_4_2, },})), print(toPrint_2_3.__boxee),     blockResult0_5_4 = void(0),     (toPrint_2_3, __blockVar_5), void(0), void(0), void(0), blockResult0_5_4)  }

  function __call_14(__call_14_p_0, __call_14_p_1) {
return (void(0), immDrop(__call_14_p_1))  }

  function immInterfaceDestructor_2(immInterfaceDestructor_2_p_0) {
return immConcreteDestructor(immInterfaceDestructor_2_p_0)  }

  function immConcreteDestructor_5(immConcreteDestructor_5_p_0) {
return ((immConcreteDestructor_5_p_0), void(0))  }

const _proto_IFunction1_AnonSubstruct = {
 _ic_IFunction1___call: __call_4,

};
const _proto_IFunction1_1_AnonSubstruct_1 = {
 _ic_IFunction1_1___call_1: __call_5,
 _ic_IFunction1_1_immInterfaceDestructor: immInterfaceDestructor_2,

};
const _proto_IFunction1_2_AnonSubstruct_2 = {
 _ic_IFunction1_2___call_2: __call_6,

};
const _proto_IFunction1_3_AnonSubstruct_3 = {
 _ic_IFunction1_3___call_3: __call_7,
 _ic_IFunction1_3_immInterfaceDestructor_1: immInterfaceDestructor_3,

};
  console.log(main());
})();