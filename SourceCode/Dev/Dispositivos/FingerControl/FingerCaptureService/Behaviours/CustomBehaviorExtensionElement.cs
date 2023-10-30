using Hangar.Core.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;

namespace FingerCaptureService.Behaviours
{
    public class CustomBehaviorExtensionElement : BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            return new HangarEndpointBehavior();
        }

        public override Type BehaviorType
        {
            get
            {
                return typeof(HangarEndpointBehavior);
            }
        }
    }
}
