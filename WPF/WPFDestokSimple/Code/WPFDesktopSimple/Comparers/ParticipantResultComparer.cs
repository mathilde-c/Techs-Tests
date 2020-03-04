
using System;
using System.Collections;
using WPFDesktopSimple.Models;
using ThirdPartySimulator;

namespace WPFDesktopSimple.Comparers
{
    public class ParticipantResultComparer : IComparer
    {
        private int[] priorities;
        public ParticipantResultComparer()
        {
            priorities = new int[(int)EndingEventType.None + 1];
            int priority = 0;
            priorities[(int)EndingEventType.Withdrawn] = priority++;
            priorities[(int)EndingEventType.Fault] = priority++;
            priorities[(int)EndingEventType.Abandon] = priority++;
            priorities[(int)EndingEventType.None] = priority++;
        }

        public int Compare(object x, object y)
        {
            ParticipantResult pr1 = x as ParticipantResult;
            ParticipantResult pr2 = y as ParticipantResult;

            if (pr1 == null || pr2 == null)
            {
                throw new ArgumentException($"cannot compare {(x == null ? "NULL" : x.GetType().ToString())} with {(y == null ? "NULL" : y.GetType().ToString())}, expected type: {typeof(ParticipantResult)}");
            }

            int comparisonResult = priorities[(int)pr1.Irm] < priorities[(int)pr2.Irm]
                                        ? 1
                                        : priorities[(int)pr1.Irm] > priorities[(int)pr2.Irm]
                                            ? -1
                                            : 0;
            
            if (comparisonResult == 0
                && pr1.NetTime.HasValue)
            {
                comparisonResult = pr1.NetTime < pr2.NetTime
                                        ? -1
                                        : pr1.NetTime > pr2.NetTime
                                            ? 1
                                            : 0;
            }
            
            if (comparisonResult == 0)
            {
                comparisonResult = pr1.Id < pr2.Id ? -1 : 1;
            }

            return comparisonResult;
        }
    }
}
