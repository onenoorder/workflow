#pragma once

#include "../../../Lexing/TokenStream.h"
#include "IStatementNode.h"

using namespace WorkflowsTraining::Lexing;

namespace WorkflowsTraining {
    namespace Parsing {
        namespace Statements {
            namespace Interfaces {

                class IParserRule {
                public:
                    virtual ~IParserRule() = default;

                    virtual bool CanParse(TokenStream& stream) const = 0;

                    virtual std::unique_ptr<IStatementNode> Parse(TokenStream& stream) const = 0;
                };
            }
        }
    }
}