﻿/*
 * QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals.
 * Lean Algorithmic Trading Engine v2.0. Copyright 2014 QuantConnect Corporation.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/

using QuantConnect.Data;
using QuantConnect.Brokerages;

namespace QuantConnect.Algorithm.CSharp
{
    /// <summary>
    /// Basic template algorithm simply initializes the date range and cash. This is a skeleton
    /// framework you can use for designing an algorithm.
    /// </summary>
    /// <meta name="tag" content="using data" />
    /// <meta name="tag" content="using quantconnect" />
    /// <meta name="tag" content="trading and orders" />
    public class BasicTemplateCryptoAlgorithm : QCAlgorithm
    {
        /// <summary>
        /// Initialise the data and resolution required, as well as the cash and start-end dates for your algorithm. All algorithms must initialized.
        /// </summary>
        public override void Initialize()
        {
            SetStartDate(2016, 10, 7);  //Set Start Date
            SetEndDate(2016, 10, 7);    //Set End Date
            SetCash(100000);             //Set Strategy Cash

            SetBrokerageModel(BrokerageName.GDAX, AccountType.Cash);

            // Find more symbols here: http://quantconnect.com/data
            AddCrypto("BTCUSD");
        }

        /// <summary>
        /// OnData event is the primary entry point for your algorithm. Each new data point will be pumped in here.
        /// </summary>
        /// <param name="data">Slice object keyed by symbol containing the stock data</param>
        public override void OnData(Slice data)
        {
            if (Time.Minute == 0)
            {
                if (!Portfolio.Invested)
                {
                    SetHoldings("BTCUSD", 1m);
                }
                else
                {
                    Liquidate();
                }

                var btcHoldings = Portfolio.CashBook["BTC"].Amount;
                Log($"{Time} - BTC holdings: {btcHoldings:F8}");
            }
        }
    }
}