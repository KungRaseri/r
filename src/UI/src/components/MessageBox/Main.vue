<template>
    <div class="chatbox">
        <MessageBox />
        <v-text-field 
            autofocus=true
            outlined=true
            color="white"
            background-color="rgba(0, 0, 0, 0.2)"
            @keydown="SendMessage($event)"
            v-model="GetInput"
        />
    </div>
</template>

<script lang="ts">
    import { Component, Vue, Prop } from 'vue-property-decorator';
    import MessageBox from './MessageBox.vue';

    @Component({
        components: {
            MessageBox
        },
    })
    export default class ChatModule extends Vue {
        input = "";
        $axios: any;

        SendMessage(value: any) {
            if (value.key === "Enter") {
                this.PostMessage(this.GetInput);
                this.GetInput = "";
            }
        }

        StoreMessage(value: string) {
            this.GetInput = value;
        }

        PostMessage(value: string) {
            console.log(value);
            this.$axios
                .post(
                    "http://framework/POST_MESSAGE",
                    value
                );
        }

        get GetInput() {
            return this.input;
        }

        set GetInput(value: string) {
            this.input = value;
        }
    }
</script>

<style scoped>
    .chatbox {
        margin-left: 1vw;
    }
</style>