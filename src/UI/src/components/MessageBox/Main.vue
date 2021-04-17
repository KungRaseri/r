<template>
    <div class="chatbox">
        <MessageBox ref="messageBox"/>
        <v-text-field 
            id="textfield"
            v-if="GetTextFieldActive"
            autofocus=true
            outlined=true
            color="white"
            background-color="rgba(0, 0, 0, 0.5)"
            @keydown="SendMessage($event)"
            v-model="GetInput"
        />
    </div>
</template>

<script lang="ts">
    import { Component, Vue, Prop, Ref } from 'vue-property-decorator';
    import MessageBox from './MessageBox.vue';

    @Component({
        components: {
            MessageBox
        },
    })
    export default class ChatModule extends Vue {
        @Ref() readonly messageBox!: MessageBox;

        isTextFieldActive = false;
        input = "";
        $axios: any;

        mounted() {
            window.addEventListener("message", (e) => {
                switch (e.data.eventName) {
                    case "TOGGLE_CHAT_MODULE":
                        this.GetTextFieldActive = true;
                        break;
                    default:
                        break;
                }
            });
        }

        SendMessage(value: any) {
            console.log(value.key);
            if (value.key === "Enter") {
                this.PostMessage(this.GetInput);
                this.GetInput = "";
                this.GetTextFieldActive = false;
                this.ResetFocus();
            } else if (value.key === "Escape") {
                this.GetTextFieldActive = false;
                this.ResetFocus();
            } else if (value.key === "PageUp") {
                this.messageBox.$el.scrollTop -= this.messageBox.$el.clientHeight;
            } else if (value.key === "PageDown") {
                this.messageBox.$el.scrollTop += this.messageBox.$el.clientHeight;
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
                    { value }
                )
                .catch((error: any) => {
                    console.log("error", error);
                });
        }

        ResetFocus() {
            this.$axios
                .post(
                    "http://framework/RESET_FOCUS",
                    {}
                )
                .catch((error: any) => {
                    console.log("error", error);
                });
        }

        get GetInput() {
            return this.input;
        }

        set GetInput(value: string) {
            this.input = value;
        }

        get GetTextFieldActive() {
            return this.isTextFieldActive;
        }

        set GetTextFieldActive(value: boolean) {
            this.isTextFieldActive = value;
        }
    }
</script>

<style scoped>
    .chatbox {
        margin-left: 1vw;
    }

    #textfield {
        color: white;
    }
</style>