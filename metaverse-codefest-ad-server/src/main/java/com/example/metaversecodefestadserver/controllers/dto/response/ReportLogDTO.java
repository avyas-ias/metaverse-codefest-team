package com.example.metaversecodefestadserver.controllers.dto.response;

public class ReportLogDTO {

    private final String sessionId;
    private final Integer viewCountTillNow;
    private final float viewTimeTillNow;

    public ReportLogDTO(String sessionId, Integer viewCountTillNow, float viewTimeTillNow) {
        this.sessionId = sessionId;
        this.viewCountTillNow = viewCountTillNow;
        this.viewTimeTillNow = viewTimeTillNow;
    }

    public String getSessionId() {
        return this.sessionId;
    }

    public Integer getViewCountTillNow() {
        return this.viewCountTillNow;
    }

    public float getViewTimeTillNow() {
        return this.viewTimeTillNow;
    }

    public boolean equals(final Object o) {
        if (o == this) return true;
        if (!(o instanceof ReportLogDTO)) return false;
        final ReportLogDTO other = (ReportLogDTO) o;
        if (!other.canEqual((Object) this)) return false;
        final Object this$sessionId = this.getSessionId();
        final Object other$sessionId = other.getSessionId();
        if (this$sessionId == null ? other$sessionId != null : !this$sessionId.equals(other$sessionId)) return false;
        final Object this$viewCountTillNow = this.getViewCountTillNow();
        final Object other$viewCountTillNow = other.getViewCountTillNow();
        if (this$viewCountTillNow == null ? other$viewCountTillNow != null : !this$viewCountTillNow.equals(other$viewCountTillNow))
            return false;
        if (Float.compare(this.getViewTimeTillNow(), other.getViewTimeTillNow()) != 0) return false;
        return true;
    }

    protected boolean canEqual(final Object other) {
        return other instanceof ReportLogDTO;
    }

    public int hashCode() {
        final int PRIME = 59;
        int result = 1;
        final Object $sessionId = this.getSessionId();
        result = result * PRIME + ($sessionId == null ? 43 : $sessionId.hashCode());
        final Object $viewCountTillNow = this.getViewCountTillNow();
        result = result * PRIME + ($viewCountTillNow == null ? 43 : $viewCountTillNow.hashCode());
        result = result * PRIME + Float.floatToIntBits(this.getViewTimeTillNow());
        return result;
    }

    public String toString() {
        return "ReportLogDTO(sessionId=" + this.getSessionId() + ", viewCountTillNow=" + this.getViewCountTillNow() + ", viewTimeTillNow=" + this.getViewTimeTillNow() + ")";
    }
}
