<template>
    <div>
        <textarea class="form-control" v-model="body" required></textarea>
        <button class="btn btn-primary" type="button" @click="addComment">Dodaj</button>
        <div class="no-data" v-if="!comments">Brak komentarzy</div>
        <div v-for="comment in comments">
            <div class="text-secondary">@comment.CreateTime</div>
            {{ comment.Body }}
        </div>
    </div>
</template>
<script>
    export default {
        props: {
            addUrl: String,
            postId: Number,
            comments: Array
        },
        data() {
            return {
                body: "",
            }
        },
        methods: {
            addComment() {

            }
        }
    }
</script>
<style lang="scss">
</style>