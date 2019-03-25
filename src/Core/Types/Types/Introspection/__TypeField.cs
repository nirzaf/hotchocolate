﻿namespace HotChocolate.Types.Introspection
{
    [Introspection]
    internal sealed class __TypeField
        : ObjectField
    {
        internal __TypeField()
            : base(IntrospectionFields.Type, d =>
            {
                d.Description("Request the type information of a single type.")
                    .Argument("name", a => a.Type<NonNullType<StringType>>())
                    .Type<__Type>()
                    .Resolver(ctx => ctx.Schema
                        .GetType<INamedType>(ctx.Argument<string>("name")));
            })
        {
        }

        public override bool IsIntrospectionField { get; } = true;
    }
}