using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumikApp.Core.Domain
{
    public class PollData : FlavoursSet
    {
        private PollPurpose _PollPurpose;
        public PollPurpose PollPurpose
        {
            get { return _PollPurpose; }
            set
            {
                if (_PollPurpose == value)
                    return;

                _PollPurpose = value;
                RaisePropertyChanged(nameof(PollPurpose));
            }
        }

        private PollMixes _PollMixes;
        public PollMixes PollMixes
        {
            get { return _PollMixes; }
            set
            {
                if (_PollMixes == value)
                    return;

                _PollMixes = value;
                RaisePropertyChanged(nameof(PollMixes));
            }
        }

        private PollPricePoints _PollPricePoints;
        public PollPricePoints PollPricePoints
        {
            get { return _PollPricePoints; }
            set
            {
                if (_PollPricePoints == value)
                    return;

                _PollPricePoints = value;
                RaisePropertyChanged(nameof(PollPricePoints));
            }
        }

        // first section - potrzebuję w celu

        private bool _ForPartyBool;
        public bool ForPartyBool
        {
            get { return _ForPartyBool; }
            set
            {
                if (_ForPartyBool == value)
                    return;

                if (value)
                {
                    GoodButCheap = false;
                    Exclusive = false;
                    ForPiratesFromCarabien = false;
                    PollPurpose = PollPurpose.ForPartyBool;
                }
                else
                {
                    if (PollPurpose == PollPurpose.ForPartyBool)
                        PollPurpose = PollPurpose.None;
                }

                _ForPartyBool = value;
                RaisePropertyChanged(nameof(ForPartyBool));
            }
        }

        private bool _GoodButCheap;
        public bool GoodButCheap
        {
            get { return _GoodButCheap; }
            set
            {
                if (_GoodButCheap == value)
                    return;

                if (value)
                {
                    ForPartyBool = false;
                    Exclusive = false;
                    ForPiratesFromCarabien = false;
                    PollPurpose = PollPurpose.GoodButCheap;
                }
                else
                {
                    if (PollPurpose == PollPurpose.GoodButCheap)
                        PollPurpose = PollPurpose.None;
                }

                _GoodButCheap = value;
                RaisePropertyChanged(nameof(GoodButCheap));
            }
        }

        private bool _Exclusive;
        public bool Exclusive
        {
            get { return _Exclusive; }
            set
            {
                if (_Exclusive == value)
                    return;

                if (value)
                {
                    ForPartyBool = false;
                    GoodButCheap = false;
                    ForPiratesFromCarabien = false;
                    PollPurpose = PollPurpose.Exclusive;
                }
                else
                {
                    if (PollPurpose == PollPurpose.Exclusive)
                        PollPurpose = PollPurpose.None;
                }

                _Exclusive = value;
                RaisePropertyChanged(nameof(Exclusive));
            }
        }

        private bool _ForPiratesFromCarabien;
        public bool ForPiratesFromCarabien
        {
            get { return _ForPiratesFromCarabien; }
            set
            {
                if (_ForPiratesFromCarabien == value)
                    return;

                if (value)
                {
                    ForPartyBool = false;
                    GoodButCheap = false;
                    Exclusive = false;
                    PollPurpose = PollPurpose.ForPiratesFromCarabien;
                }
                else
                {
                    if (PollPurpose == PollPurpose.ForPiratesFromCarabien)
                        PollPurpose = PollPurpose.None;
                }

                _ForPiratesFromCarabien = value;
                RaisePropertyChanged(nameof(ForPiratesFromCarabien));

                //if (value)
                //    GoForPiratesFromCarabien();
            }
        }

        // second section - do picia
        private bool _solo;
        public bool solo
        {
            get { return _solo; }
            set
            {
                if (_solo == value)
                    return;

                if (value)
                {
                    PollMixes = PollMixes.Solo;
                    WithCoke = false;
                }
                else
                {
                    if (PollMixes == PollMixes.Solo)
                        PollMixes = PollMixes.None;
                }

                _solo = value;
                RaisePropertyChanged(nameof(solo));
            }
        }

        private bool _WithCoke;
        public bool WithCoke
        {
            get { return _WithCoke; }
            set
            {
                if (_WithCoke == value)
                    return;
                if (value)
                {
                    PollMixes = PollMixes.WithCoke;
                    solo = false;
                }
                else
                {
                    if (PollMixes == PollMixes.WithCoke)
                        PollMixes = PollMixes.None;
                }

                _WithCoke = value;
                RaisePropertyChanged(nameof(WithCoke));
            }
        }

        // fourth section - price
        private bool _PricePoint1;
        public bool PricePoint1
        {
            get { return _PricePoint1; }
            set
            {
                if (_PricePoint1 == value)
                    return;
                if (value)
                {
                    PricePoint2 = false;
                    PricePoint3 = false;
                    PricePoint4 = false;
                    PollPricePoints = PollPricePoints.PricePointTo50;
                }
                else
                {
                    if (PollPricePoints == PollPricePoints.PricePointTo50)
                        PollPricePoints = PollPricePoints.None;
                }

                _PricePoint1 = value;
                RaisePropertyChanged(nameof(PricePoint1));
            }
        }

        private bool _PricePoint2;
        public bool PricePoint2
        {
            get { return _PricePoint2; }
            set
            {
                if (_PricePoint2 == value)
                    return;

                if (value)
                {
                    PricePoint1 = false;
                    PricePoint3 = false;
                    PricePoint4 = false;
                    PollPricePoints = PollPricePoints.PricePoint50To70;
                }
                else
                {
                    if (PollPricePoints == PollPricePoints.PricePoint50To70)
                        PollPricePoints = PollPricePoints.None;
                }

                _PricePoint2 = value;
                RaisePropertyChanged(nameof(PricePoint2));
            }
        }

        private bool __PricePoint3;
        public bool PricePoint3
        {
            get { return __PricePoint3; }
            set
            {
                if (__PricePoint3 == value)
                    return;

                if (value)
                {
                    PricePoint1 = false;
                    PricePoint2 = false;
                    PricePoint4 = false;
                    PollPricePoints = PollPricePoints.PricePoint70To90;
                }
                else
                {
                    if (PollPricePoints == PollPricePoints.PricePoint70To90)
                        PollPricePoints = PollPricePoints.None;
                }

                __PricePoint3 = value;
                RaisePropertyChanged(nameof(PricePoint3));
            }
        }

        private bool __PricePoint4;
        public bool PricePoint4
        {
            get { return __PricePoint4; }
            set
            {
                if (__PricePoint4 == value)
                    return;

                if (value)
                {
                    PricePoint1 = false;
                    PricePoint2 = false;
                    PricePoint3 = false;
                    PollPricePoints = PollPricePoints.PricePointFrom90;
                }
                else
                {
                    if (PollPricePoints == PollPricePoints.PricePointFrom90)
                        PollPricePoints = PollPricePoints.None;
                }

                __PricePoint4 = value;
                RaisePropertyChanged(nameof(PricePoint4));
            }
        }


    }
}
