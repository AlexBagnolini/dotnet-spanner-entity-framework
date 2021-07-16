// Copyright 2020, Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;

namespace Google.Cloud.EntityFrameworkCore.Spanner.Update.Internal
{
    public class NoSavepointsTransactionFactory : IRelationalTransactionFactory
    {
        public virtual RelationalTransaction Create(
            IRelationalConnection connection,
            DbTransaction transaction,
            Guid transactionId,
            IDiagnosticsLogger<DbLoggerCategory.Database.Transaction> logger,
            bool transactionOwned)
            => new NoSavepointsTransaction(connection, transaction, transactionId, logger, transactionOwned);

        class NoSavepointsTransaction : RelationalTransaction
        {
            public NoSavepointsTransaction(
                IRelationalConnection connection,
                DbTransaction transaction,
                Guid transactionId,
                IDiagnosticsLogger<DbLoggerCategory.Database.Transaction> logger,
                bool transactionOwned)
                : base(connection, transaction, transactionId, logger, transactionOwned)
            {
            }

            public override bool SupportsSavepoints => false;
        }
    }
}