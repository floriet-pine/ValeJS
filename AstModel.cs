using System.Collections.Generic;
using System.Linq;

public class AstModel : IProgram,
                        IStruct,
                        IEdge,
                        IInterfaceId,
                        IStructId,
                        IFunction,
                        IPrototype, 
                        IBlock,
                        IReturn,
                        ICall,
                        IInterfaceCall,
                        IConsecutor,
                        IStackify,
                        IUnstackify,
                        IDiscard,
                        IExternCall,
                        IArgument,
                        ILocal,
                        ILocalLoad,
                        ILocalStore,
                        IVariableId,
                        INewStruct,
                        IConstant,
                        IMemberLoad,
                        IMemberStore,
                        IIf,
                        IWhile,
                        INewArrayFromValues,
                        IArrayLoad,
                        IConstructUnknownSizeArray {

  public string __type { get; set; }
  public string Name { get; set; }

  public AstModel InterfaceName { get; set; }
  IInterfaceId IEdge.InterfaceName => InterfaceName;
  public AstModel StructName { get; set; }
  IStructId IEdge.StructName => StructName;

  public AstModel Prototype { get; set; }
  IPrototype IFunction.Prototype => Prototype;

  public AstModel Block { get; set; }
  public List<AstModel> Interfaces { get; set; }
  public List<AstModel> Externs { get; set; }

  public List<AstModel> Structs { get; set; }
  List<IStruct> IProgram.Structs => Structs.Select<AstModel, IStruct>(x => x).ToList();


  public AstModel Function { get; set; }
  public List<AstModel> Functions { get; set; }
  List<IFunction> IProgram.Functions => Functions.Select<AstModel, IFunction>(x => x).ToList();

  public List<AstModel> Edges { get; set; }
  List<IEdge> IStruct.Edges => Edges.Select<AstModel, IEdge>(x => x).ToList();

  public List<AstModel> ImmDestructorsByKind { get; set; }

  public AstModel Return { get; set; }
  public List<AstModel> Params { get; set; }

  public AstModel Ownership { get; set; }
  public AstModel Location { get; set; }
  public AstModel Referend { get; set; }

  public AstModel InnerExpr { get; set; }
  public AstModel SourceExpr { get; set; }
  public List<AstModel> SourceExprs { get; set; }
  public AstModel SourceType { get; set; }
  public AstModel StructExpr { get; set; }

  public AstModel Local { get; set; }
  ILocal IStackify.Local => Local;
  ILocal IUnstackify.Local => Local;
  ILocal ILocalLoad.Local => Local;
  ILocal ILocalStore.Local => Local;

  public AstModel Id { get; set; }
  IVariableId ILocal.Id => Id;

  public int Number { get; set; }
  public int MemberIndex { get; set; }

  public List<AstModel> ArgExprs { get; set; }
  public List<AstModel> Exprs { get; set; }
  public AstModel OptName { get; set; }
  public AstModel SourceResultType { get; set; }
  public AstModel ResultType { get; set; }

  public string Value { get; set; }
  public int ArgumentIndex { get; set; }

  public string MemberName { get; set; }
  public List<string> MemberNames { get; set; }

  public AstModel ConditionBlock { get; set; }
  public AstModel ThenBlock { get; set; }
  public AstModel ElseBlock { get; set; }
  public AstModel BodyBlock { get; set; }
  IBlock IWhile.BodyBlock => BodyBlock;

  public List<AstModel> Methods { get; set; }
  public AstModel Override { get; set; }
  public AstModel InterfaceRef { get; set; }
  //IPrototype IInterfaceMethod.Override => Override;
  //public int VirtualParamIndex { get; set; }
  public AstModel Method { get; set; }

  public AstModel FunctionType { get; set; }
  IPrototype IInterfaceCall.FunctionType => FunctionType;

  public AstModel ArrayExpr { get; set; }
  public AstModel IndexExpr { get; set; }

  public AstModel SizeExpr { get; set; }
  public AstModel GeneratorExpr { get; set; }
}

public interface IExpression {
  string __type { get; }
}

public interface IProgram {
  List<AstModel> Interfaces { get; }
  List<IStruct> Structs { get; }
  List<AstModel> Externs { get; }
  List<IFunction> Functions { get; }
  List<AstModel> ImmDestructorsByKind { get; }
}

public interface IStruct : IExpression {
  string Name { get; }
  List<IEdge> Edges { get; }
}

public interface IEdge : IExpression {
  IInterfaceId InterfaceName { get; }
  IStructId StructName { get; }
  List<AstModel> Methods { get; }
}

public interface IInterfaceId : IExpression {
  string Name { get; }
}

public interface IStructId : IExpression {
  string Name { get; }
}

public interface IFunction : IExpression {
  IPrototype Prototype { get; }
  AstModel Block { get; }
}

public interface IBlock : IExpression {
  AstModel InnerExpr { get; }
  AstModel SourceExpr { get; }
}

public interface IPrototype : IExpression {
  string Name { get; }
  List<AstModel> Params { get; }
}

public interface IRef : IExpression {
  AstModel Ownership { get; }
  AstModel Location { get; }
  AstModel Referend { get; }
}

public interface IReturn : IExpression {
  AstModel SourceExpr { get; }
  AstModel SourceType { get; }
}

public interface ICall : IExpression {
  AstModel Function { get; }
  List<AstModel> ArgExprs { get; }
}

public interface IInterfaceCall : IExpression {
  List<AstModel> ArgExprs { get; }
  IPrototype FunctionType { get; }
  AstModel InterfaceRef { get; }
}

public interface IConsecutor : IExpression {
  List<AstModel> Exprs { get; }
}

public interface IStackify : IExpression {
  ILocal Local { get; }
  AstModel OptName { get; }
  AstModel SourceExpr { get; }
}
public interface IUnstackify : IExpression {
  ILocal Local { get; }
}
public interface IDiscard : IExpression {
 AstModel SourceExpr { get; }
}

public interface IExternCall : IExpression {
  AstModel Function { get; }
  List<AstModel> ArgExprs { get; }
}

public interface IArgument : IExpression {
  int ArgumentIndex { get; }
}

public interface ILocal : IExpression {
  IVariableId Id { get; }
}

public interface IVariableId : IExpression {
  int Number { get; }
  AstModel OptName { get; }
}

public interface ILocalLoad : IExpression {
 ILocal Local { get; } 
}
public interface ILocalStore : IExpression {
 ILocal Local { get; } 
 AstModel SourceExpr { get; }
}

public interface INewStruct : IExpression {
  List<AstModel> SourceExprs { get; }
  List<string> MemberNames { get; }
  AstModel ResultType { get; }
}

public interface IConstant : IExpression {
  string Value { get; }
}

public interface IMemberLoad : IExpression {
  AstModel StructExpr { get; }  
  string MemberName { get; }
}

public interface IMemberStore : IExpression {
 AstModel SourceExpr { get; }
 AstModel StructExpr { get; }
 string MemberName { get; }
}

public interface IIf : IExpression {
  AstModel ConditionBlock { get; }
  AstModel ThenBlock { get; }
  AstModel ElseBlock { get; }
}
public interface IWhile : IExpression {
  IBlock BodyBlock { get; }
}

public interface INewArrayFromValues : IExpression {
  List<AstModel> SourceExprs { get; }
}

public interface IArrayLoad : IExpression {
  AstModel ArrayExpr { get; }
  AstModel IndexExpr { get; }
}

public interface IConstructUnknownSizeArray : IExpression {
  AstModel SizeExpr { get; }
  AstModel GeneratorExpr { get; }
}