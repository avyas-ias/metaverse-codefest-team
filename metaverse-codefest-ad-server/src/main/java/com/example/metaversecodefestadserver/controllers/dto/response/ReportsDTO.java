package com.example.metaversecodefestadserver.controllers.dto.response;

import java.util.List;

public class ReportsDTO {

    private final List<AdReportDTO> sessionReports;

    private final List<ReportLogDTO> logReports;

    public ReportsDTO(List<AdReportDTO> sessionReports, List<ReportLogDTO> logReports) {
        this.sessionReports = sessionReports;
        this.logReports = logReports;
    }

    public List<AdReportDTO> getSessionReports() {
        return this.sessionReports;
    }

    public List<ReportLogDTO> getLogReports() {
        return this.logReports;
    }

    public boolean equals(final Object o) {
        if (o == this) return true;
        if (!(o instanceof ReportsDTO)) return false;
        final ReportsDTO other = (ReportsDTO) o;
        if (!other.canEqual((Object) this)) return false;
        final Object this$sessionReports = this.getSessionReports();
        final Object other$sessionReports = other.getSessionReports();
        if (this$sessionReports == null ? other$sessionReports != null : !this$sessionReports.equals(other$sessionReports))
            return false;
        final Object this$logReports = this.getLogReports();
        final Object other$logReports = other.getLogReports();
        if (this$logReports == null ? other$logReports != null : !this$logReports.equals(other$logReports))
            return false;
        return true;
    }

    protected boolean canEqual(final Object other) {
        return other instanceof ReportsDTO;
    }

    public int hashCode() {
        final int PRIME = 59;
        int result = 1;
        final Object $sessionReports = this.getSessionReports();
        result = result * PRIME + ($sessionReports == null ? 43 : $sessionReports.hashCode());
        final Object $logReports = this.getLogReports();
        result = result * PRIME + ($logReports == null ? 43 : $logReports.hashCode());
        return result;
    }

    public String toString() {
        return "ReportsDTO(sessionReports=" + this.getSessionReports() + ", logReports=" + this.getLogReports() + ")";
    }
}
