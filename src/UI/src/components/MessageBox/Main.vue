<template>
    <div class="chatbox">
        <v-slide-y-transition>
            <MessageBox ref="messageBox"
                        v-show="GetMessageBoxActive"
                        @showmessages="Timeout"
                        @timeout="Timeout" />
        </v-slide-y-transition>
        <v-text-field v-show="GetTextFieldActive"
                      autofocus=true
                      outlined=true
                      dark
                      dense
                      height="20px"
                      background-color="rgba(0, 0, 0, 0.5)"
                      @keydown="SendMessage($event)"
                      v-model="GetInput" />
    </div>
</template>

<script lang="ts">
    import { Component, Vue, Prop, Ref, VModel } from 'vue-property-decorator';
    import MessageBox from './MessageBox.vue';

    @Component({
        components: {
            MessageBox
        },
    })
    export default class ChatModule extends Vue {
        @Ref() readonly messageBox!: MessageBox;

        isTextFieldActive = false;
        isMessageBoxActive = false;
        input = "";
        $axios: any;

        mounted() {
            window.addEventListener("message", (e) => {
                switch (e.data.eventName) {
                    case "TOGGLE_CHAT_MODULE":
                        this.GetTextFieldActive = true;
                        this.GetMessageBoxActive = true;
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
                this.Timeout(false);
                this.ResetFocus();
            } else if (value.key === "PageUp") {
                this.messageBox.$el.scrollTop -= this.messageBox.$el.clientHeight / 3;
            } else if (value.key === "PageDown") {
                this.messageBox.$el.scrollTop += this.messageBox.$el.clientHeight / 3;
            }
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

        Timeout(value: boolean) {
            if (value === false) {
                setTimeout(this.SetMessageBoxActive, 5000, value);
            } else {
                this.GetMessageBoxActive = value;
            }
        }

        SetMessageBoxActive(value: boolean) {
            this.GetMessageBoxActive = value;
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

        get GetMessageBoxActive() {
            return this.isMessageBoxActive;
        }

        set GetMessageBoxActive(value: boolean) {
            this.isMessageBoxActive = value;
        }
    }
</script>

<style scoped>
    .chatbox {
        margin-left: 1vw;
    }
</style>