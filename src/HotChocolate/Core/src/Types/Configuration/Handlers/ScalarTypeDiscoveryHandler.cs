#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using HotChocolate.Internal;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;

namespace HotChocolate.Configuration;

internal sealed class ScalarTypeDiscoveryHandler : TypeDiscoveryHandler
{
    public ScalarTypeDiscoveryHandler(ITypeInspector typeInspector)
    {
        TypeInspector = typeInspector ?? throw new ArgumentNullException(nameof(typeInspector));
    }

    private ITypeInspector TypeInspector { get; }

    public override bool TryInferType(
        ITypeReference typeReference,
        TypeDiscoveryInfo typeInfo,
        [NotNullWhen(true)] out ITypeReference[]? schemaTypeRefs)
    {
        if (Scalars.TryGetScalar(typeInfo.RuntimeType, out var scalarType))
        {
            var schemaTypeRef = TypeInspector.GetTypeRef(scalarType);
            schemaTypeRefs = new ITypeReference[] { schemaTypeRef };
            return true;
        }

        schemaTypeRefs = null;
        return false;
    }

    public override bool TryInferKind(
        ITypeReference typeReference,
        TypeDiscoveryInfo typeInfo,
        out TypeKind typeKind)
    {
        if (Scalars.TryGetScalar(typeInfo.RuntimeType, out _))
        {
            typeKind = TypeKind.Scalar;
            return true;
        }

        typeKind = default;
        return false;
    }
}