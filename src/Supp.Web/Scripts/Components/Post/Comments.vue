<template>
    <div>
        <div class="no-data" v-if="!comments">Brak komentarzy</div>
        <div v-for="comment in comments" class="comment">
            <div class="text-secondary">{{ comment.createTime }} - {{ comment.author }}</div>
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
            postId: Number,
        },
        data() {
            return {
                body: "",
                comments: []
            }
        },
        created() {
            Ajax.apiRequest(this.getAllUrl, {}, r => this.comments = r.data);
        },
        methods: {
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

            }
        }
    }
</script>
<style lang="scss">
    .comment {
        margin: 15px 0;
    }
</style>