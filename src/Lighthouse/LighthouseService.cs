// Copyright 2014-2015 Aaron Stannard, Petabridge LLC
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
using System.Threading.Tasks;
using Akka.Actor;

namespace Lighthouse
{
    public class LighthouseService
    {
        private readonly string _actorSystemName;
        private readonly string _ipAddress;
        private readonly int? _port;
        private readonly int? _petabridgePort;

        private ActorSystem _lighthouseSystemOne;
        private ActorSystem _lighthouseSystemTwo;

        public LighthouseService() : this(null, null, null, null) { }

        public LighthouseService(string actorSystemName, string ipAddress, int? port, int? petabridgePort)
        {
            _actorSystemName = actorSystemName;
            _ipAddress = ipAddress;
            _port = port;
            _petabridgePort = petabridgePort;
        }

        public void Start()
        {
            _lighthouseSystemOne = LighthouseHostFactory.LaunchLighthouse("actorSystemOne", "127.0.0.1", 4053, 4063);
            _lighthouseSystemTwo = LighthouseHostFactory.LaunchLighthouse("actorSystemTwo", "127.0.0.1", 4054, 4064);
        }

        public async Task StopAsync()
        {
            await _lighthouseSystemOne.Terminate();
            await _lighthouseSystemTwo.Terminate();
        }
    }
}
