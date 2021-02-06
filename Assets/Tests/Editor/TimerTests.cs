using System;
using NUnit.Framework;
using UniRx;
using UnityEngine;

namespace Tests.Editor
{
    public class TimerTests
    {
        [Test]
        public void Timer_SpeedUp()
        {
            var timer = Observable.Timer(TimeSpan.FromSeconds(1))
                .Subscribe(_ =>
                {
                    Debug.Log("has call");
                    //call = true;
                });
        }
    }
}