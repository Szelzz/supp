<template>
    <div>
        <input v-model="newTag" type="text" />
        <button @click="addTag">Dodaj</button>
        <ul>
            <li v-for="tag in currentTags">{{ tag }}<button @click="removeTag(tag)">X</button> </li>
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