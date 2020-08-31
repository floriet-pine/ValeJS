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
  function __ext___getch() { if (!window) { return 5; } while (true) { const result = window.prompt('Press a key'); if (typeof result === 'string' && result.length !== 0) { return result.charCodeAt(0); } } }
  function makeBoard() {
    let funcResult_1_0;
    return             funcResult_1_0 = Arr(10, Object.create({}, {}));
      return funcResult_1_0;
;
  }

  function immConcreteDestructor_1(immConcreteDestructor_1_p_0) {
                    return void(0);
  }

  function __call_8(__call_8_p_0, __call_8_p_1, __call_8_p_2) {
    let closure_0;
    let rowI_1_1;
    let row_1_2;
    let blockResult0_3_3;
        closure_0 = __call_8_p_0;
        rowI_1_1 = __call_8_p_1;
        row_1_2 = __call_8_p_2;
    eachI_1(row_1_2, Object.create({}, { rowI: { writable: true, value: rowI_1_1, }, playerRow_1: { writable: true, value: closure_0.playerRow, }, playerCol_1: { writable: true, value: closure_0.playerCol, }, toPrint_1: { writable: true, value: closure_0.toPrint, },}));        closure_0.toPrint.__boxee = $43__1(closure_0.toPrint.__boxee, '\n');        blockResult0_3_3 = void(0);
                    return blockResult0_3_3;
  }

  function __addIntInt(__addIntInt_p_0, __addIntInt_p_1) {
    return __ext___addIntInt(__addIntInt_p_0, __addIntInt_p_1);
  }

  function __not(__not_p_0) {
    return __ext___not(__not_p_0);
  }

  function len_1(len_1_p_0) {
    let arr_2_0;
    let blockResult0_6_1;
        arr_2_0 = len_1_p_0;
        blockResult0_6_1 = arr_2_0.length;
            return blockResult0_6_1;
  }

  function main() {
    let board_0;
    let playerRow_2_1;
    let playerCol_2_2;
    let running_3;
    let key_4;
    let newPlayerRow_5;
    let newPlayerCol_6;
    let blockResult0_7;
    let blockResult1_4;
            board_0 = makeBoard();
            playerRow_2_1 = 4;
            playerCol_2_2 = 3;
            running_3 = true;
    while (((running_3) ? function(){
            display(board_0, playerRow_2_1, playerCol_2_2);                  key_4 = inputKey();
                  newPlayerRow_5 = playerRow_2_1;
                  newPlayerCol_6 = playerCol_2_2;
      (($61_$61_(key_4, 81)) ? function(){
                running_3 = false;
        
} : function(){
return (($61_$61_(key_4, 119)) ? function(){
                    newPlayerRow_5 = $45_(newPlayerRow_5, 1);
          
} : function(){
return (($61_$61_(key_4, 115)) ? function(){
                        newPlayerRow_5 = $43_(newPlayerRow_5, 1);
            
} : function(){
return (($61_$61_(key_4, 97)) ? function(){
                            newPlayerCol_6 = $45_(newPlayerCol_6, 1);
              
} : function(){
return (($61_$61_(key_4, 100)) ? function(){
                                newPlayerCol_6 = $43_(newPlayerCol_6, 1);
                
} : function(){
return void(0);
})()
;
})()
;
})()
;
})()
;
})()
      (($61_$61__1(board_0[newPlayerRow_5][newPlayerCol_6], '.')) ? function(){
                playerRow_2_1 = newPlayerRow_5;
                playerCol_2_2 = newPlayerCol_6;
        
} : function(){
return void(0);
})()
            blockResult0_7 = void(0);
                        return blockResult0_7;
      return true;

} : function(){
return false;
})()
) {}
        blockResult1_4 = 0;
                destructor(board_0);        return blockResult1_4;
  }

  function immConcreteDestructor_7(immConcreteDestructor_7_p_0) {
                return void(0);
  }

  function Arr_1(arr_1_p_0, arr_1_p_1) {
    let n_1_0;
    let generator_1_1;
    let blockResult1_2_4;
    let __blockVar_3;
    let tempVarName0_1_2;
        n_1_0 = arr_1_p_0;
        generator_1_1 = arr_1_p_1;
        blockResult1_2_4 =         __blockVar_3 = Array_1(n_1_0, (function(){        tempVarName0_1_2 = IFunction1_5(generator_1_1);
    return tempVarName0_1_2;
})());
    destructor_2(tempVarName0_1_2);    return __blockVar_3;
;
                return blockResult1_2_4;
  }

  function drop(drop_p_0) {
    destructor_3(drop_p_0);        return void(0);
  }

  function not(not_p_0) {
    let output_0;
    let blockResult0_11_1;
        output_0 = not_p_0;
        blockResult0_11_1 = __not(output_0);
            return blockResult0_11_1;
  }

  function __eqIntInt(__eqIntInt_p_0, __eqIntInt_p_1) {
    return __ext___eqIntInt(__eqIntInt_p_0, __eqIntInt_p_1);
  }

  function __call_3(__call_3_p_0, __call_3_p_1) {
    return __call_3_p_0._ic_IFunction1_3___call_3(__call_3_p_0, __call_3_p_1);
  }

  function immDrop(immDrop_p_0) {
            return void(0);
  }

  function constructor_1() {
    return Object.create(_proto_IFunction1_1_AnonSubstruct_1, { anonSubstructMember0_1: { writable: true, value: Object.create({}, {}), },});
  }

  function immConcreteDestructor_3(immConcreteDestructor_3_p_0) {
            return ;
                return void(0);
  }

  function immConcreteDestructor(immConcreteDestructor_p_0) {
                    return void(0);
  }

  function __call_9(__call_9_p_0, __call_9_p_1, __call_9_p_2) {
    let closure_1_0;
    let cellI_1;
    let cell_2;
    let charToPrint_3;
    let blockResult0_5_4;
        closure_1_0 = __call_9_p_0;
        cellI_1 = __call_9_p_1;
        cell_2 = __call_9_p_2;
            charToPrint_3 = cell_2;
    ((and($61_$61_(closure_1_0.rowI, closure_1_0.playerRow_1), $61_$61_(cellI_1, closure_1_0.playerCol_1))) ? function(){
            charToPrint_3 = '@';
      
} : function(){
return void(0);
})()
        closure_1_0.toPrint_1.__boxee = $43__1(closure_1_0.toPrint_1.__boxee, charToPrint_3);        blockResult0_5_4 = void(0);
                        return blockResult0_5_4;
  }

  function __call_1(__call_1_p_0, __call_1_p_1) {
    return __call_1_p_0._ic_IFunction1_1___call_1(__call_1_p_0, __call_1_p_1);
  }

  function $60_($60__p_0, $60__p_1) {
    let left_2_0;
    let right_2_1;
    let blockResult0_16_2;
        left_2_0 = $60__p_0;
        right_2_1 = $60__p_1;
        blockResult0_16_2 = __lessThanInt(left_2_0, right_2_1);
                return blockResult0_16_2;
  }

  function __getch() {
    return __ext___getch();
  }

  function and(and_p_0, and_p_1) {
    let left_1_0;
    let right_1_1;
    let blockResult0_15_2;
        left_1_0 = and_p_0;
        right_1_1 = and_p_1;
        blockResult0_15_2 = __and(left_1_0, right_1_1);
                return blockResult0_15_2;
  }

  function constructor() {
    return Object.create(_proto_IFunction1_3_AnonSubstruct_3, { anonSubstructMember0_3: { writable: true, value: Object.create({}, {}), },});
  }

  function $61_$61__1($61_$61__1_p_0, $61_$61__1_p_1) {
    let left_3_0;
    let right_3_1;
    let blockResult0_19_2;
        left_3_0 = $61_$61__1_p_0;
        right_3_1 = $61_$61__1_p_1;
        blockResult0_19_2 = __eqStrStr(left_3_0, right_3_1);
                return blockResult0_19_2;
  }

  function __call_4(__call_4_p_0, __call_4_p_1) {
    return __call_11(__call_4_p_0.anonSubstructMember0, __call_4_p_1);
  }

  function inputKey() {
    let key_1_0;
    let done_1;
    let funcResult_2;
            key_1_0 = 0;
            done_1 = false;
    while (((not(done_1)) ? function(){
                  key_1_0 = __getch();
      (($61_$61_(key_1_0, 81)) ? function(){
                done_1 = true;
        
} : function(){
return (($61_$61_(key_1_0, 119)) ? function(){
                    done_1 = true;
          
} : function(){
return (($61_$61_(key_1_0, 115)) ? function(){
                        done_1 = true;
            
} : function(){
return (($61_$61_(key_1_0, 97)) ? function(){
                            done_1 = true;
              
} : function(){
return (($61_$61_(key_1_0, 100)) ? function(){
                                done_1 = true;
                
} : function(){
return (($61_$61_(key_1_0, 10)) ? function(){
return void(0);
} : function(){
return void(0);
})()
;
})()
;
})()
;
})()
;
})()
;
})()
            return true;

} : function(){
return false;
})()
) {}
        return             funcResult_2 = key_1_0;
                  return funcResult_2;
;
  }

  function __call_2(__call_2_p_0, __call_2_p_1) {
    return __call_2_p_0._ic_IFunction1_2___call_2(__call_2_p_0, __call_2_p_1);
  }

  function immInterfaceDestructor_3(immInterfaceDestructor_3_p_0) {
    return immConcreteDestructor_1(immInterfaceDestructor_3_p_0);
  }

  function Arr(arr_p_0, arr_p_1) {
    let n_0;
    let generator_1;
    let blockResult1_1_4;
    let __blockVar_3;
    let tempVarName0_2;
        n_0 = arr_p_0;
        generator_1 = arr_p_1;
        blockResult1_1_4 =         __blockVar_3 = ValeArray(n_0, (function(){        tempVarName0_2 = IFunction1_4(generator_1);
    return tempVarName0_2;
})());
    destructor_1(tempVarName0_2);    return __blockVar_3;
;
                return blockResult1_1_4;
  }

  function immConcreteDestructor_6(immConcreteDestructor_6_p_0) {
                    return void(0);
  }

  function $43__1($43__1_p_0, $43__1_p_1) {
    let a_0;
    let b_1;
    let blockResult0_13_2;
        a_0 = $43__1_p_0;
        b_1 = $43__1_p_1;
        blockResult0_13_2 = __addStrStr(a_0, b_1);
                return blockResult0_13_2;
  }

  function __call_12(__call_12_p_0, __call_12_p_1) {
            return drop(__call_12_p_1);
  }

  function __call_5(__call_5_p_0, __call_5_p_1) {
    return __call_13(__call_5_p_0.anonSubstructMember0_1, __call_5_p_1);
  }

  function IFunction1_5(iFunction1_5_p_0) {
    return Object.create(_proto_IFunction1_AnonSubstruct, { anonSubstructMember0: { writable: true, value: iFunction1_5_p_0, },});
  }

  function $45_($45__p_0, $45__p_1) {
    let left_0;
    let right_1;
    let blockResult0_10_2;
        left_0 = $45__p_0;
        right_1 = $45__p_1;
        blockResult0_10_2 = __subtractIntInt(left_0, right_1);
                return blockResult0_10_2;
  }

  function __eqStrStr(__eqStrStr_p_0, __eqStrStr_p_1) {
    return __ext___eqStrStr(__eqStrStr_p_0, __eqStrStr_p_1);
  }

  function immConcreteDestructor_5(immConcreteDestructor_5_p_0) {
                return void(0);
  }

  function immConcreteDestructor_2(immConcreteDestructor_2_p_0) {
            return ;
                    return void(0);
  }

  function $43_($43__p_0, $43__p_1) {
    let a_1_0;
    let b_1_1;
    let blockResult0_14_2;
        a_1_0 = $43__p_0;
        b_1_1 = $43__p_1;
        blockResult0_14_2 = __addIntInt(a_1_0, b_1_1);
                return blockResult0_14_2;
  }

  function destructor_1(destructor_1_p_0) {
                    return void(0);
  }

  function __addStrStr(__addStrStr_p_0, __addStrStr_p_1) {
    return __ext___addStrStr(__addStrStr_p_0, __addStrStr_p_1);
  }

  function __call_7(__call_7_p_0, __call_7_p_1) {
    return __call_12(__call_7_p_0.anonSubstructMember0_3, __call_7_p_1);
  }

  function destructor(destructor_p_0) {
            return void(0);
  }

  function ValeArray(valeArray_p_0, valeArray_p_1) {
    let size_0;
    let generator_2_1;
    let blockResult0_17_2;
        size_0 = valeArray_p_0;
        generator_2_1 = valeArray_p_1;
        blockResult0_17_2 = (function(){
const __size = size_0;
const __generator = generator_2_1;
const __arr = new Array(__size);
for(let __i = 0; __i < __size; __i++) {
  __arr[__i] = __generator._ic_IFunction1_2___call_2(__generator, __i);
}
return __arr;
})();
                return blockResult0_17_2;
  }

  function __lessThanInt(__lessThanInt_p_0, __lessThanInt_p_1) {
    return __ext___lessThanInt(__lessThanInt_p_0, __lessThanInt_p_1);
  }

  function eachI_1(eachI_1_p_0, eachI_1_p_1) {
    let arr_1_0;
    let func_1_1;
    let i_1_2;
    let l_1_3;
    let blockResult0_4_4;
        arr_1_0 = eachI_1_p_0;
        func_1_1 = eachI_1_p_1;
            i_1_2 = 0;
            l_1_3 = len_1(arr_1_0);
    while ((($60_(i_1_2, l_1_3)) ? function(){
            __call_9(func_1_1, i_1_2, arr_1_0[i_1_2]);            i_1_2 = $43_(i_1_2, 1);
            return true;

} : function(){
return false;
})()
) {}
        blockResult0_4_4 = void(0);
                        return blockResult0_4_4;
  }

  function Array_1(array_1_p_0, array_1_p_1) {
    let size_1_0;
    let generator_3_1;
    let blockResult0_18_2;
        size_1_0 = array_1_p_0;
        generator_3_1 = array_1_p_1;
        blockResult0_18_2 = (function(){
const __size = size_1_0;
const __generator = generator_3_1;
const __arr = new Array(__size);
for(let __i = 0; __i < __size; __i++) {
  __arr[__i] = __generator._ic_IFunction1___call(__generator, __i);
}
return __arr;
})();
                return blockResult0_18_2;
  }

  function __call_10(__call_10_p_0, __call_10_p_1) {
    let closure_2_0;
    let row_2_1;
    let blockResult0_8_2;
        closure_2_0 = __call_10_p_0;
        row_2_1 = __call_10_p_1;
        blockResult0_8_2 = Arr_1(10, Object.create({}, { row: { writable: true, value: row_2_1, },}));
                return blockResult0_8_2;
  }

  function IFunction1_4(iFunction1_4_p_0) {
    return Object.create(_proto_IFunction1_2_AnonSubstruct_2, { anonSubstructMember0_2: { writable: true, value: iFunction1_4_p_0, },});
  }

  function __print(__print_p_0) {
    return __ext___print(__print_p_0);
  }

  function $61_$61_($61_$61__p_0, $61_$61__p_1) {
    let left_4_0;
    let right_4_1;
    let blockResult0_20_2;
        left_4_0 = $61_$61__p_0;
        right_4_1 = $61_$61__p_1;
        blockResult0_20_2 = __eqIntInt(left_4_0, right_4_1);
                return blockResult0_20_2;
  }

  function print(print_p_0) {
    let output_1_0;
    let blockResult0_12_1;
        output_1_0 = print_p_0;
        blockResult0_12_1 = __print(output_1_0);
            return blockResult0_12_1;
  }

  function eachI(eachI_p_0, eachI_p_1) {
    let arr_0;
    let func_1;
    let i_2;
    let l_3;
    let blockResult0_2_4;
        arr_0 = eachI_p_0;
        func_1 = eachI_p_1;
            i_2 = 0;
            l_3 = len(arr_0);
    while ((($60_(i_2, l_3)) ? function(){
            __call_8(func_1, i_2, arr_0[i_2]);            i_2 = $43_(i_2, 1);
            return true;

} : function(){
return false;
})()
) {}
        blockResult0_2_4 = void(0);
                        return blockResult0_2_4;
  }

  function len(len_p_0) {
    let arr_3_0;
    let blockResult0_7_1;
        arr_3_0 = len_p_0;
        blockResult0_7_1 = arr_3_0.length;
            return blockResult0_7_1;
  }

  function destructor_3(destructor_3_p_0) {
            return void(0);
  }

  function __subtractIntInt(__subtractIntInt_p_0, __subtractIntInt_p_1) {
    return __ext___subtractIntInt(__subtractIntInt_p_0, __subtractIntInt_p_1);
  }

  function __and(__and_p_0, __and_p_1) {
    return __ext___and(__and_p_0, __and_p_1);
  }

  function destructor_2(destructor_2_p_0) {
                    return void(0);
  }

  function immInterfaceDestructor(immInterfaceDestructor_p_0) {
    return immInterfaceDestructor_p_0._ic_IFunction1_1_immInterfaceDestructor(immInterfaceDestructor_p_0);
  }

  function __call(__call_p_0, __call_p_1) {
    return __call_p_0._ic_IFunction1___call(__call_p_0, __call_p_1);
  }

  function immInterfaceDestructor_1(immInterfaceDestructor_1_p_0) {
    return immInterfaceDestructor_1_p_0._ic_IFunction1_3_immInterfaceDestructor_1(immInterfaceDestructor_1_p_0);
  }

  function __call_11(__call_11_p_0, __call_11_p_1) {
    let closure_3_0;
    let col_1;
    let blockResult0_9_2;
        closure_3_0 = __call_11_p_0;
        col_1 = __call_11_p_1;
        blockResult0_9_2 = (($61_$61_(closure_3_0.row, 0)) ? function(){
return '#';
} : function(){
return (($61_$61_(col_1, 0)) ? function(){
return '#';
} : function(){
return (($61_$61_(closure_3_0.row, 9)) ? function(){
return '#';
} : function(){
return (($61_$61_(col_1, 9)) ? function(){
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
;
                return blockResult0_9_2;
  }

  function display(display_p_0, display_p_1, display_p_2) {
    let board_1_0;
    let playerRow_3_1;
    let playerCol_3_2;
    let toPrint_2_3;
    let blockResult0_1_4;
    let __blockVar_5;
        board_1_0 = display_p_0;
        playerRow_3_1 = display_p_1;
        playerCol_3_2 = display_p_2;
            toPrint_2_3 = Object.create({}, { __boxee: { writable: true, value: '', },});
    eachI(board_1_0, Object.create({}, { toPrint: { writable: true, value: toPrint_2_3, }, playerRow: { writable: true, value: playerRow_3_1, }, playerCol: { writable: true, value: playerCol_3_2, },}));    print(toPrint_2_3.__boxee);        blockResult0_1_4 = void(0);
                return __blockVar_5;
                    return blockResult0_1_4;
  }

  function __call_13(__call_13_p_0, __call_13_p_1) {
            return immDrop(__call_13_p_1);
  }

  function __call_6(__call_6_p_0, __call_6_p_1) {
    return __call_10(__call_6_p_0.anonSubstructMember0_2, __call_6_p_1);
  }

  function immInterfaceDestructor_2(immInterfaceDestructor_2_p_0) {
    return immConcreteDestructor(immInterfaceDestructor_2_p_0);
  }

  function immConcreteDestructor_4(immConcreteDestructor_4_p_0) {
                return void(0);
  }

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