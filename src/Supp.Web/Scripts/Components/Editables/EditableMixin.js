export default {
    props: {
        'canEdit': {
            type: Boolean,
            default: true
        },
        'updateUrl': String,
        'propertyName': String,
        'modelId': Number,
        'modelValue': String,
        'template': {
            type: String,
            default: 'text'
        }
    },
    data() {
        return {
            editMode: false,
            value: this.modelValue,
            displayControl: null,
            editControl: null
        };
    },
    methods: {
        buttonMouseOver() {
            this.$refs.editableArea.classList.add("editable-area-hover");
        },
        buttonMouseOut() {
            this.$refs.editableArea.classList.remove("editable-area-hover");
        },
        getValue() {
            return this.editControl.value;
        },
        edit() {
            this.editMode = true;
        },
        save() {
            fetch(this.updateUrl,
                {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    cache: 'no-cache',
                    body: this.dataToJson()
                }
            )
                .then(r => r.json())
                .then(this.success)
        },
        success(response) {
            if (this.onSuccess)
                this.onSuccess(response);
            else
                this.value = response;

            this.editMode = false;
        },
        dataToJson() {
            let obj = {
                modelId: this.modelId,
                propertyName: this.propertyName,
                value: this.value
            };
            return JSON.stringify(obj);
        }
    }
}