using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNet.SignalR.Hubs;
using VoteR.Models;
using System.Data.Entity;

namespace VoteR
{
   
public class VoteHub : Hub 
{
    private static List<Item> VoteItems = new List<Item>();
    private static VoteRContext db = new VoteRContext();

    public void Vote(int id)
    {   
        // AddVote tabulates the vote         
        var votes = AddVote(id);
        // Clients.All.updateVoteResults notifies all clients that someone has voted and the page updates itself to relect that 
        Clients.All.updateVoteResults(id, votes);
    }
    private static Item AddVote(int id) {

        // If the item is in VoteItems, we're tracking it, so just increment and return
        var voteItem = VoteItems.Find(v => v.Id == id);        
        if (voteItem != null)
        {
            voteItem.Votes++;
            return voteItem; 
        }
        // If the item wasn't in VoteItems, it's the first time someone voted for it. 
        // Add it to VoteItems and increment from zero
        else
        {
            var item = db.Items.Find(id);
            item.Votes++;
            VoteItems.Add(item);
            return item;   
        }        
    }
    public override Task OnConnected()
    {        
        // Send voting history to caller only so they can see an updated view of the votes   
        Clients.Caller.joinVoting(VoteItems.ToList());
        return base.OnConnected();
    }
}


}