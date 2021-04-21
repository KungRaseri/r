<template>
    <div class="chatbox">
        <div class="message">
            <v-slide-y-transition>
                <MessageBox ref="messageBox"
                            v-show="GetMessageBoxActive"
                            @showmessages="Timeout"
                            @timeout="Timeout"
                            :is-text-field-active="GetTextFieldActive" />
            </v-slide-y-transition>
        </div>         
        <v-textarea v-show="GetTextFieldActive"
                      autofocus=true
                      outlined=true
                      dark
                      dense
                      auto-grow
                      rows="1"
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
        history = new Set("");
        historyCount = 0;
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
                if (this.GetInput !== "") {
                    this.PostMessage(this.GetInput);
                    this.AddHistory(this.GetInput);
                    this.GetHistoryCount = this.history.size;
                }
                this.GetInput = "";
                this.GetTextFieldActive = false;
                this.ResetFocus();
            } else if (value.key === "Escape") {
                this.GetTextFieldActive = false;
                this.ResetFocus();
            } else if (value.key === "PageUp") {
                this.messageBox.$el.scrollTop -= this.messageBox.$el.clientHeight / 3;
            } else if (value.key === "PageDown") {
                this.messageBox.$el.scrollTop += this.messageBox.$el.clientHeight / 3;
            } else if (value.key === "Home") {
                this.messageBox.$el.scrollTop = 0;
            } else if (value.key === "End") {
                this.setScroll();
            } else if (value.key === "ArrowUp") {
                this.ReturnHistory(-1);
            } else if (value.key === "ArrowDown") {
                this.ReturnHistory(1);
            }
        }

        ReturnHistory(value: number) {
            let tempHistory = Array.from(this.history);
            this.GetHistoryCount += value;
            this.GetInput = tempHistory[this.GetHistoryCount];
        }

        PostMessage(value: string) {
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
            if (!value) {
                this.SetMessageBoxActive(value);
            } else {
                this.GetMessageBoxActive = value;
            }
        }

        async SetMessageBoxActive(value: boolean) {
            let j = 100;
            for (let i = 0; i <= 5000; i += j) {
                if (this.GetTextFieldActive) {
                    return;
                }
                await this.delay(j);
            }
            this.GetMessageBoxActive = value;
        }

        private delay(ms: number) {
            return new Promise(resolve => setTimeout(resolve, ms));
        }

        setScroll() {
            this.messageBox.$el.scrollTop = this.messageBox.$el.scrollHeight;
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
            this.Timeout(value);
        }

        get GetMessageBoxActive() {
            return this.isMessageBoxActive;
        }

        set GetMessageBoxActive(value: boolean) {
            this.isMessageBoxActive = value;
            if (value) {
                this.$nextTick(this.setScroll);
            }
        }

        get GetHistoryCount() {
            return this.historyCount;
        }

        set GetHistoryCount(value: number) {
            if (value < 0) {
                this.historyCount = 0;
            } else if (value > this.history.size) {
                this.historyCount = this.history.size;
            } else {
                this.historyCount = value;
            }
        }

        AddHistory(value: string) {
            if (this.history.has(value)) {
                this.history.delete(value);
            }
            this.history.add(value);
        }
    }
</script>

<style>
    .message {
        overflow: hidden;
        height: 30vh;
        width: 25vw;
    }
</style>

<style scoped>
    .chatbox {
        margin-left: 3vw;
    }
</style>