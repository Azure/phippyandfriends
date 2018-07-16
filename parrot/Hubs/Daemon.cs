using System.Threading.Tasks;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using parrot.Models;

namespace parrot
{
    public class DaemonHub : Hub
    {
        static List<Pod> Pods { get; set; }
        static List<string> DeletedPods { get; set; }

        static DaemonHub()
        {
            Pods = new List<Pod>();
            DeletedPods = new List<string>();
        }

        const string POD_DELETED_STATUS = "Deleted";

        public override Task OnConnectedAsync()
        {
            Clients.All.SendAsync("clusterViewUpdated", Pods);
            return base.OnConnectedAsync();
        }

        public void AddPod(Pod pod)
        {
            if(!DeletedPods.Contains(pod.Name))
            {
                Pods.Add(pod);
            }
        }

        public void RemovePod(Pod pod)
        {
            Pods.Remove(Pods.First(x => x.Name == pod.Name));
            DeletedPods.Add(pod.Name);
        }

        public void UpdatePod(Pod pod)
        {
            Pods.First(x => x.Name == pod.Name).Name = pod.Name;
            Pods.First(x => x.Name == pod.Name).Container = pod.Container;
            Pods.First(x => x.Name == pod.Name).NameSpace = pod.NameSpace;
            Pods.First(x => x.Name == pod.Name).Status = pod.Status;
        }

        public void clearClusterView()
        {
            Pods.Clear();
            Clients.All.SendAsync("clusterViewUpdated", Pods);
        }

        public void updateClusterView(Pod pod)
        {
            // If the container image is "image:tag", strip the ":tag", otherwise leave it alone
            // not all images are tagged, so..
            if(pod.ContainerImage.Contains(':'))
                pod.ContainerImage = pod.ContainerImage.Substring(0, pod.ContainerImage.IndexOf(':'));

            if (Pods.Any(x => x.Name == pod.Name))
                if (pod.Action == POD_DELETED_STATUS)
                    RemovePod(pod);
                else
                    UpdatePod(pod);
            else
                AddPod(pod);

                Clients.All.SendAsync("clusterViewUpdated", Pods);
        }
    }
}