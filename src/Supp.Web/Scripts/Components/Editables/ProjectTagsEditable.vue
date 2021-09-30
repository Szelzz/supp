<template>
    <div>
        <div>
            <div class="input-group mb-3">
                <input v-model="newTag" type="text" class="form-control" placeholder="Nazwa tagu">
                <button @click="addTag" class="btn btn-outline-secondary" type="button" id="button-addon2">Dodaj</button>
            </div>
        </div>
        <ul class="list-group">
            <li class="list-group-item" v-for="tag in currentTags">
                {{ tag }}
                <button class="btn-close float-end" @click="removeTag(tag)"></button>
            </li>
        </ul>
    </div>
</template>
<script>
    import Ajax from '../../Ajax';
    export default {
        props: {
            projectId: Number,
            tags: Array,
            removeUrl: String,
            addUrl: String
        },
        data() {
            return {
                newTag: "",
                currentTags: this.tags,
            }
        },
        created() {
        },
        methods: {
            addTag() {
                Ajax.apiRequest(
                    this.addUrl,
                    { projectId: this.projectId, tagName: this.newTag },
                    this.onAddTagSuccess)
                this.newTag = "";
            },
            onAddTagSuccess(newTag) {
                this.currentTags.push(newTag);
            },
            removeTag(tag) {
                Ajax.apiRequest(
                    this.removeUrl,
                    { projectId: this.projectId, tagName: tag })

                const index = this.currentTags.indexOf(tag);
                if (index > -1) {
                    this.currentTags.splice(index, 1);
                }
            }
        }
    }
</script>