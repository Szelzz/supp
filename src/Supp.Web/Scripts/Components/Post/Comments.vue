<template>
    <div>
        <div class="no-data" v-if="!comments">Brak komentarzy</div>
        <div v-for="comment in commentsSorted" class="comment bg-opacity-10" :class="{ 'pinned': comment.pinned }">
            <div class="text-secondary">
                {{ comment.createTime }} - {{ comment.author }}
                <button v-if="comment.pinned" class="btn btn-sm btn-outline-secondary float-end" @click="unpinComment(comment)"><i class="fas fa-times"></i></button>
                <button v-else type="button" class="btn btn-sm btn-outline-secondary float-end" @click="pinComment(comment)"><i class="fas fa-thumbtack"></i></button>
            </div>
            {{ comment.body }}
        </div>
        <textarea class="form-control" v-model="body" required></textarea>
        <button class="btn btn-primary mt-1" type="button" @click="addComment" :disabled="body == ''">Dodaj komentarz</button>
    </div>
</template>
<script>
    import Ajax from '../../Ajax'

    export default {
        props: {
            addUrl: String,
            getAllUrl: String,
            pinCommentUrl: String,
            unpinCommentUrl: String,
            postId: Number,
        },
        data() {
            return {
                body: "",
                comments: []
            }
        },
        computed: {
            commentsSorted() {
                return this.comments.sort(this.compareComments);
            }
        },
        created() {
            Ajax.apiRequest(this.getAllUrl, {}, r => this.comments = r.data);
        },
        methods: {
            compareComments(a, b) {
                if (a.pinned == b.pinned)
                    return a.id - b.id;
                if (a.pinned && !b.pinned)
                    return -1;
                if (!a.pinned && b.pinned)
                    return 1;
            },
            addComment() {
                Ajax.apiRequest(this.addUrl, {
                    postId: this.postId,
                    body: this.body,
                }, this.success, this.error)
            },
            success(response) {
                this.comments.push(response.data);
                this.body = "";
            },
            error(response) {

            },
            pinComment(comment) {
                Ajax.apiRequest(this.pinCommentUrl,
                    comment.id);
                comment.pinned = true;
            },
            unpinComment(comment) {
                Ajax.apiRequest(this.unpinCommentUrl,
                    comment.id);
                comment.pinned = false;
            }
        }
    }
</script>
<style lang="scss">
    @import '../../../Styles/colors.scss';

    .comment {
        margin: 10px 0;
        padding: 5px;

        &.pinned {
            border: 3px solid $success;
            border-radius: 0.25rem;
        }
    }
</style>