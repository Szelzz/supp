<template>
    <div class="editable-area d-flex" ref="editableArea">
        <div class="flex-fill">
            <span v-show="!editMode">
                <i v-if="currentTags.length == 0">
                    brak przypisanych kategorii
                </i>
                <span v-for="tag in currentTags">#{{ tag }} </span>
            </span>
            <div v-show="editMode">
                <div class="d-inline-block">
                    <button class="btn btn-sm btn-dark me-1"
                            v-for="tag in currentTags"
                            @click="removeTag(tag)">
                        #{{ tag }} <i class="fas fa-times"></i>
                    </button>
                </div>
                <div class="d-inline-block">
                    <select @change="addTag($event.target.value)" class="form-select form-select-sm" style="width:200px">
                        <option></option>
                        <option :value="tag" v-for="tag in allTags">{{ tag }}</option>
                    </select>
                </div>
            </div>
        </div>
        <span>
            <button @click="edit" v-show="!editMode" class="btn btn-sm btn-outline-dark" @mouseover="buttonMouseOver" @mouseout="buttonMouseOut">
                <i class="fas fa-pen-square"></i>
            </button>
            <button @click="save" v-show="editMode" class="btn btn-sm btn-outline-dark" @mouseover="buttonMouseOver" @mouseout="buttonMouseOut">
                <i class="fas fa-save"></i>
            </button>
        </span>
    </div>
</template>

<script>
    import EditableMixin from './EditableMixin'

    export default {
        mixins: [EditableMixin],
        props: {
            tags: Array,
            allTags: Array
        },
        data() {
            return {
                currentTags: this.tags,
            }
        },
        created() {
            this.setValue();
        },
        methods: {
            addTag(tag) {
                this.currentTags.push(tag);
                this.setValue();
            },
            removeTag(tag) {
                const index = this.currentTags.indexOf(tag);
                if (index > -1) {
                    this.currentTags.splice(index, 1);
                }
                this.setValue();
            },
            setValue() {
                this.value = this.currentTags.join(',');
            },
            onSuccess(response) {
                this.currentTags = response.split(',').filter(t => t != '');
                this.setValue();
            }
        }
    }
</script>

<style lang="scss">
    .editable-area-hover {
        background-color: rgba(0, 0, 0, 0.10);
    }
</style>