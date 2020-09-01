# Generation

**Fix non-simple Tups**
Simple tups is currently created as 'void 0' (undefined). But more advanced tup's is not recognized, and creates an empty object instead

**Multiple returns is happening (often in destructor) (Possibly fixed)**

** Instances where `varname.` is written (dot in the end) as parameter name **

# Features

** Generation/consume for arrays **

Especially GenerateDestroyKnownSizeArrayIntoFunction, which is hardcoded to just give void(0) right now.