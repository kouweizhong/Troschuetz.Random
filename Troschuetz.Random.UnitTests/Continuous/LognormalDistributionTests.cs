﻿// The MIT License (MIT)
//
// Copyright (c) 2006-2007 Stefan Troschütz <stefan@troschuetz.de>
//
// Copyright (c) 2012-2017 Alessio Parma <alessio.parma@gmail.com>
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
// associated documentation files (the "Software"), to deal in the Software without restriction,
// including without limitation the rights to use, copy, modify, merge, publish, distribute,
// sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT
// NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT
// OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

namespace Troschuetz.Random.Tests.Continuous
{
    using Distributions.Continuous;
    using NUnit.Framework;
    using System;

    public sealed class LognormalDistributionTests : ContinuousDistributionTests<LognormalDistribution>
    {
        protected override LognormalDistribution GetDist(LognormalDistribution other = null)
        {
            return new LognormalDistribution { Mu = GetMu(other), Sigma = GetSigma(other) };
        }

        protected override LognormalDistribution GetDist(uint seed, LognormalDistribution other = null)
        {
            return new LognormalDistribution(seed) { Mu = GetMu(other), Sigma = GetSigma(other) };
        }

        protected override LognormalDistribution GetDist(IGenerator gen, LognormalDistribution other = null)
        {
            return new LognormalDistribution(gen) { Mu = GetMu(other), Sigma = GetSigma(other) };
        }

        protected override LognormalDistribution GetDistWithParams(LognormalDistribution other = null)
        {
            return new LognormalDistribution(GetMu(other), GetSigma(other));
        }

        protected override LognormalDistribution GetDistWithParams(uint seed, LognormalDistribution other = null)
        {
            return new LognormalDistribution(seed, GetMu(other), GetSigma(other));
        }

        protected override LognormalDistribution GetDistWithParams(IGenerator gen, LognormalDistribution other = null)
        {
            return new LognormalDistribution(gen, GetMu(other), GetSigma(other));
        }

        [TestCase(double.NaN)]
        [TestCase(TinyNeg)]
        [TestCase(SmallNeg)]
        [TestCase(LargeNeg)]
        public void Sigma_WrongValues(double d)
        {
            Assert.False(LognormalDistribution.AreValidParams(1, d));
            Assert.False(Dist.IsValidSigma(d));
            Assert.Throws<ArgumentOutOfRangeException>(() => { Dist.Sigma = d; });
        }

        // any value
        private double GetMu(IMuDistribution<double> d)
        {
            return d == null ? Rand.NextDouble(-10, 10) : d.Mu;
        }

        // sigma >= 0, better keep it low
        private double GetSigma(ISigmaDistribution<double> d)
        {
            return d == null ? Rand.NextDouble() : d.Sigma;
        }
    }
}