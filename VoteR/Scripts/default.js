$(function () {
    var voteHub = $.connection.voteHub;

    $.connection.hub.start().done(function () {
        $("button").click(function () {
            voteHub.server.vote(this.id);
        });
    });

    voteHub.client.updateVoteResults = function (id, vote) {
        var voteFor = $("span[data-itemid='" + vote.Id + "']");
        voteFor[0].textContent = vote.Votes;
    }

    voteHub.client.joinVoting = function (votes) {
        joinVoting(votes);
    }

    function joinVoting(votes) {
        for (i = 0; i <= votes.length - 1; i++) {
            var voteFor = $("span[data-itemid='" + votes[i].Id + "']");
            voteFor[0].textContent = votes[i].Votes;
        }
    } 
});

