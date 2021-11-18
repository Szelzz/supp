<template>
    <div class="d-flex flex-column vote-component">
        <div class="placeholder" v-if="voted">&nbsp;</div>
        <button class="btn-vote btn"
                @click="vote" v-if="!voted && canVote" title="zagłosuj">
            <i class="fas fa-caret-up"></i>
        </button>
        <div class="votes" :class="{ voted: voted }">
            {{ votes }}
        </div>
        <button class="btn-vote btn"
                @click="undo" v-if="voted && canVote" title="cofnij głos">
            <i class="fas fa-caret-down"></i>
        </button>
    </div>
</template>
<script>
    import Ajax from '../../Ajax'

    export default {
        props: {
            initVoted: Boolean,
            initVotes: Number,
            voteUrl: String,
            undoUrl: String,
            canVote: Boolean
        },
        data() {
            return {
                votes: this.initVotes,
                voted: this.initVoted
            }
        },
        methods: {
            vote() {
                Ajax.apiRequest(this.voteUrl, {}, this.votedSuccess);
            },
            undo() {
                Ajax.apiRequest(this.undoUrl, {}, this.undoSuccess);
            },
            votedSuccess() {
                this.voted = true;
                this.votes++;
            },
            undoSuccess() {
                this.voted = false;
                this.votes--;
            }
        }
    }
</script>
<style lang="scss">
    @import '../../../Styles/colors.scss';

    $upvoteSize: 1.5rem;

    .vote-component {
        width: 80px;
        position: absolute;
        left: -90px;

        .placeholder {
            font-size: $upvoteSize;
        }
    }

    .btn-vote {
        font-size: $upvoteSize;
        padding: 0;
    }

    .votes {
        text-align: center;
        border: 2px solid;
        border-color: $primary;
        border-radius: 100px;
        font-size: 1.2rem;
        padding: 5px 0;
    }

    .votes.voted {
        background-color: $primary;
        color: $white;
    }
</style>