<script>
    import Editable from './Components/Editables/Editable.vue'
    import PostTagEditable from './Components/Editables/PostTagEditable.vue'
    import Votes from './Components/Post/Votes.vue'
    import Comments from './Components/Post/Comments.vue'
    import Search from './Components/Post/Search.vue'
    import PostList from './Components/Post/PostList.vue'
    import ProjectTagsEditable from './Components/Editables/ProjectTagsEditable.vue'
    import ProjectPermissionSettings from './Components/Editables/ProjectPermissionSettings.vue'

    export default {
        el: '#app',
        components: {
            Editable,
            PostTagEditable,
            Votes,
            Comments,
            Search,
            PostList,
            ProjectTagsEditable,
            ProjectPermissionSettings
        }
    }
</script>

<style lang="scss" scoped>
</style>